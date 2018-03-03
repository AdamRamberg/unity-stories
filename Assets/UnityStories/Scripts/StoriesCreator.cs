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
        private List<Action<Story>> mapStoriesToPropsHandlers = new List<Action<Story>>();
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
            stories.GetStories = DefImpl_GetStories;

			if (entryStory != null) entryStory.InitStory();
		}

        private StoryAction DefImpl_Dispatch(StoryAction action)
        {
			// Send action to reducers
			foreach (var subStory in entryStory.subStories)
			{
				SendToReducers(action, subStory, reducers);
			}

            // Send update to everyone that have mapped their props
            foreach (var handler in mapStoriesToPropsHandlers)
            {
                handler(entryStory);
            }

            // Send action forward to listeners
            foreach (var handler in actionListeners)
            {
                handler(action);
            }

			return action;
        }

		private void SendToReducers(StoryAction action, Story story, Reducer[] reducers)
		{
			foreach (var reducer in reducers)
            {
				if (story.Name == reducer.Name) 
				{
					reducer.Handler(story, action);

					if (story.subStories != null)
					{
						foreach (var subStory in story.subStories) 
						{
							SendToReducers(action, subStory, reducer.subReducers);
						}
					}
				}
            }
		}

        public void DefImpl_Connect(Action<Story> handler)
        {
            mapStoriesToPropsHandlers.Add(handler);
            // Send stories on connect
            handler(entryStory);
        }

		public int DefImpl_GetConnectedCount() { return mapStoriesToPropsHandlers.Count; }

		public void DefImpl_Listen(Action<StoryAction> handler)
		{
			actionListeners.Add(handler);
		}

        public EntryStory DefImpl_GetStories()
        {
            return entryStory;
        }
    }
}