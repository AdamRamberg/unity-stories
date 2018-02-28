using UnityEngine;

namespace UnityStories 
{
    public abstract class EnhancerCreator : ScriptableObject
    {
        public delegate StoreCreator.DelegateCreateStore Enhancer(StoreCreator.DelegateCreateStore createStore);
        public abstract Enhancer CreateEnhancer();
    }
}