using System;

namespace UnityStories 
{
    [Serializable]
    public class Enhancer
    {
        public delegate StoreCreator.DelegateCreateStore DelegateEnhance(StoreCreator.DelegateCreateStore createStore);
        public DelegateEnhance Enhance;
        protected StoreCreator.DelegateCreateStore _Enhance(StoreCreator.DelegateCreateStore createStore)
        {
            return null;
        }
    }
}