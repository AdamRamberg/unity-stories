using UnityEngine;

namespace UnityStories 
{
    public abstract class Middleware : ScriptableObject
    {
        public class MiddlewareAPI 
        {
            public Stories.DelegateDispatch Dispatch;
            public Stories.DelegateGetStories GetStories;
        }
        
        public delegate Compose.DelegateComposedDispatch DelegateMiddleware(MiddlewareAPI middlewareAPI);
        public delegate DelegateMiddleware DelegateReturnMiddleware();
        public DelegateReturnMiddleware ReturnMiddleware;

        public Middleware() 
        {
            ReturnMiddleware = _ReturnMiddleware;
        }

        public abstract DelegateMiddleware _ReturnMiddleware();
    }
}