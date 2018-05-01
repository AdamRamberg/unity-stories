using System;
using UnityEngine;
using UnityStories;

namespace UnityStories
{
    [Serializable]
    public class StoriesHelper
    {
        public Stories stories;

        private Action<Story> mapStoriesToProps;
        private Action<StoryAction> onStoryAction;
        private DetectOnDestroy detectOnDestroy;
        private const string STORIES_WARNING = "Stories is not assigned when trying to setup stories helper.";
        private const string GAMEOBJECT_ERROR = "GameObject can't be null.";
        private const string DISPATCH_WARNING = "Dispatching but stories was not assigned.";

        public void Setup(GameObject gameObject, Action<Story> mapStoriesToProps = null, Action<StoryAction> onStoryAction = null)
        {
            if (gameObject == null)
            {
                throw new Exception(GAMEOBJECT_ERROR);
            }

            if (stories == null)
            {
                Debug.LogWarning(STORIES_WARNING);
                return;
            }

            if (mapStoriesToProps != null)
            {
                this.mapStoriesToProps = mapStoriesToProps;
                stories.Connect(mapStoriesToProps);
            }

            if (onStoryAction != null)
            {
                this.onStoryAction = onStoryAction;
                stories.Listen(onStoryAction);
            }

            var detector = gameObject.GetComponent<DetectOnDestroy>();
            if (detector != null)
            {
                detectOnDestroy = detector;
            }
            else 
            {
                detectOnDestroy = gameObject.AddComponent<DetectOnDestroy>();
            }

            detectOnDestroy.OnDestroyAction += OnDestroy;
        }

        public void Dispatch(StoryAction storyAction)
        {
            if (stories == null)
            {
                Debug.LogWarning(DISPATCH_WARNING);
                return;
            }

            stories.Dispatch(storyAction);
        }

        private void OnDestroy()
        {
            if (mapStoriesToProps != null)
            {
                stories.Disconnect(mapStoriesToProps);
            }

            if (onStoryAction != null)
            {
                stories.RemoveListener(onStoryAction);
            }

            detectOnDestroy.OnDestroyAction -= OnDestroy;
        }
    }
}