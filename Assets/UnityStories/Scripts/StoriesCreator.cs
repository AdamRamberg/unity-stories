using System;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

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
			stories.Disconnect = DefImpl_Disconnect;
			stories.Listen = DefImpl_Listen;
			stories.RemoveListener = DefImpl_RemoveListener;
			stories.GetConnectedCount = DefImpl_GetConnectedCount;
            stories.GetStories = DefImpl_GetStories;

			if (entryStory != null) entryStory.InitStory();
		}

        private StoryAction DefImpl_Dispatch(StoryAction action)
        {
			// Apply action to stories
			ApplyActionToStories(action, entryStory);

            // Send update to everyone that have mapped their props
            for (var i = 0; i < mapStoriesToPropsHandlers.Count; ++i)
            {
                mapStoriesToPropsHandlers[i](entryStory);
            }

            // Send action forward to listeners
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

		public void DefImpl_Disconnect(Action<Story> handler)
        {
            mapStoriesToPropsHandlers.Remove(handler);
        }

		public int DefImpl_GetConnectedCount() { return mapStoriesToPropsHandlers.Count; }

		public void DefImpl_Listen(Action<StoryAction> handler)
		{
			actionListeners.Add(handler);
		}

		public void DefImpl_RemoveListener(Action<StoryAction> handler)
		{
			actionListeners.Remove(handler);
		}

        public EntryStory DefImpl_GetStories()
        {
            return entryStory;
        }
    }

	#if UNITY_EDITOR
	[CustomPropertyDrawer(typeof(StoriesCreator))]
	public class StoriesCreatorDrawer : PropertyDrawer
	{
		const float PADDING = 4f;
		const float BUTTON_HEIGHT = 20f;
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    	{
			StoriesCreator storiesCreator = fieldInfo.GetValue(property.serializedObject.targetObject) as StoriesCreator;
			
			// Add generate entry story button
			var buttonPos = new Rect(new Vector2(position.x, position.y + EditorGUI.GetPropertyHeight(property) + PADDING), new Vector2(position.width, BUTTON_HEIGHT));
			if (storiesCreator.entryStory == null && GUI.Button(buttonPos, "Create Entry Story")) 
			{
				var entryStory = ScriptableObject.CreateInstance<EntryStory>();
				AssetDatabase.CreateAsset(entryStory, StoriesUtils.GetSelectedPathOrFallback() + "/EntryStory.asset");
        		AssetDatabase.SaveAssets();
				storiesCreator.entryStory = entryStory;
			}
			
			// Draw default 
			EditorGUI.PropertyField(position, property, label, true);
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			StoriesCreator storiesCreator = fieldInfo.GetValue(property.serializedObject.targetObject) as StoriesCreator;
			if (storiesCreator.entryStory != null) {
				return EditorGUI.GetPropertyHeight(property);
			}
			return EditorGUI.GetPropertyHeight(property) + BUTTON_HEIGHT + PADDING + PADDING;
		}
	}
	#endif
}