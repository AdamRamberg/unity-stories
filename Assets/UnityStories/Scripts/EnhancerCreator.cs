using UnityEngine;

namespace UnityStories 
{
    public abstract class EnhancerCreator : ScriptableObject
    {
        public delegate StoriesCreator.DelegateCreateStories Enhancer(StoriesCreator.DelegateCreateStories createStore);
        public abstract Enhancer CreateEnhancer();
    }
}