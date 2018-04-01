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
			if (enhancerCreators == null || enhancerCreators.Length == 0) return null;

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
			// Apply action to stories
			ApplyActionToStories(action, entryStory);

            // // Send update to everyone that have mapped their props
            for (var i = 0; i < mapStoriesToPropsHandlers.Count; ++i)
            {
                mapStoriesToPropsHandlers[i](entryStory);
            }

            // // Send action forward to listeners
            for (var i = 0; i < actionListeners.Count; ++i)
            {
                actionListeners[i](action);
            }

			action.ReleaseActionForReuse();
			return action;
        }

		private void ApplyActionToStories(StoryAction action, Story story)
		{
			action.ApplyToStory(story);

			if (story.subStories == null) return;

			for (var i = 0; i < story.subStories.Length; ++i) 
			{
				ApplyActionToStories(action, story.subStories[i]);
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