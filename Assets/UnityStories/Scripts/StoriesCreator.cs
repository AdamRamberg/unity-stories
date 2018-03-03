using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStories 
{
    [Serializable]
    public class StoriesCreator 
    {
        public EntryStory entryStory;
		public EnhancerCreator[] enhancerCreators;
        private List<Action<Story>> mapStoriesToPropsHandlers = new List<Action<Story>>();
        private List<Action<StoryAction>> actionListeners = new List<Action<StoryAction>>();

		public delegate void DelegateCreateStories(Stories stories, EnhancerCreator.Enhancer enhancer = null);

		public void CreateStories(Stories stories) 
		{
			_CreateStories(stories, GetComposedEnhancers());
		}

		private EnhancerCreator.Enhancer GetComposedEnhancers()
		{
			if (enhancerCreators == null) return null;

			var enhancers = new EnhancerCreator.Enhancer[enhancerCreators.Length];
			for (var i = 0; i < enhancerCreators.Length; ++i) 
			{
				enhancers[i] = enhancerCreators[i].CreateEnhancer();
			}

			return Compose.ComposeEnhancers(enhancers);
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
			// Send action to stories
			SendActionToStories(action, entryStory);

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

		private void SendActionToStories(StoryAction action, Story story)
		{
			story.ActionHandler(action);

			if (story.subStories == null) return;

			foreach (var subStory in story.subStories)
			{
				SendActionToStories(action, subStory);
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