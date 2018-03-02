using System;

namespace UnityStories 
{
    [Serializable]
    public class Enhancer
    {
        public delegate StoriesCreator.DelegateCreateStories DelegateEnhance(StoriesCreator.DelegateCreateStories createStore);
        public DelegateEnhance Enhance;
        protected StoriesCreator.DelegateCreateStories _Enhance(StoriesCreator.DelegateCreateStories createStore)
        {
            return null;
        }
    }
}