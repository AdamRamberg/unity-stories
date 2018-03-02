using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStories 
{
    [Serializable]
    public class StoriesCreator 
    {
        public Reducer[] reducers;
        public EntryStory entryStory;
		// TODO: Make into array
		public EnhancerCreator enhancerCreators;
        private List<Action<Story>> mapStateToPropsHandlers = new List<Action<Story>>();
        private List<Action<StoryAction>> actionListeners = new List<Action<StoryAction>>();

		public delegate void DelegateCreateStories(Stories stories, EnhancerCreator.Enhancer enhancer = null);

		public void CreateStories(Stories stories) 
		{
			_CreateStories(stories, enhancerCreators != null ? enhancerCreators.CreateEnhancer() : null);
		}
		
		private void _CreateStories(Stories stories, EnhancerCreator.Enhancer enhancer = null)
		{
			if (enhancer != null) 
			{
				enhancer(_CreateStories)(stories);
				return;
			}

			stories.Dispatch = DefImpl_Dispatch;
			stories.Connect = DefImpl_Connect;
			stories.Listen = DefImpl_Listen;
			stories.GetConnectedCount = DefImpl_GetConnectedCount;
            stories.GetState = DefImpl_GetState;

			if (entryStory != null) entryStory.InitStory();
		}

        private StoryAction DefImpl_Dispatch(StoryAction action)
        {
			// Send action to reducers
			foreach (var subState in entryStory.subStories)
			{
				SendToReducers(action, subState, reducers);
			}

            // Send update to everyone that have mapped their props
            foreach (var handler in mapStateToPropsHandlers)
            {
                handler(entryStory);
            }

            // Send action forward to listeners
            foreach (var handler in actionListeners)
            {
                handler(action);
            }

			#if UNITY_EDITOR
            RepaintEditor();
			#endif

			return action;
        }

		private void SendToReducers(StoryAction action, Story state, Reducer[] reducers)
		{
			foreach (var reducer in reducers)
            {
				if (state.Name == reducer.Name) 
				{
					reducer.Handler(state, action);

					if (state.subStories != null)
					{
						foreach (var subState in state.subStories) 
						{
							SendToReducers(action, subState, reducer.subReducers);
						}
					}
				}
            }
		}

		#if UNITY_EDITOR
		// Repaint unity editor
		public delegate void RepaintEditorAction();
		public event RepaintEditorAction WantRepaintEditor;
		private void RepaintEditor()
		{
			if (WantRepaintEditor != null)
			{
				WantRepaintEditor();
			}
		}
		#endif

        public void DefImpl_Connect(Action<Story> handler)
        {
            mapStateToPropsHandlers.Add(handler);
            // Send state on connect
            handler(entryStory);
        }

		public int DefImpl_GetConnectedCount() { return mapStateToPropsHandlers.Count; }

		public void DefImpl_Listen(Action<StoryAction> handler)
		{
			actionListeners.Add(handler);
		}

        public EntryStory DefImpl_GetState()
        {
            return entryStory;
        }
    }
}