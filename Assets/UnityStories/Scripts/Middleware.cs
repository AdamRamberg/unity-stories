using UnityEngine;

namespace UnityStories 
{
    public abstract class Middleware : ScriptableObject
    {
        public class MiddlewareAPI 
        {
            public Store.DelegateDispatch Dispatch;
            public Store.DelegateGetState GetState;
        }
        
        public delegate Compose.DelegateComposedDispatch DelegateMiddleware(MiddlewareAPI api);
        public delegate DelegateMiddleware DelegateReturnMiddleware();
        public DelegateReturnMiddleware ReturnMiddleware;

        public Middleware() 
        {
            ReturnMiddleware = _ReturnMiddleware;
        }

        public abstract DelegateMiddleware _ReturnMiddleware();
    }
}