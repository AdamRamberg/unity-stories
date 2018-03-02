using UnityEngine;

namespace UnityStories 
{
    [CreateAssetMenu(menuName = "UnityStories/Middleware/Logger")]
    public class Logger : Middleware
    {
        public Logger() 
        {
            ReturnMiddleware = _ReturnMiddleware;
        }

        public override DelegateMiddleware _ReturnMiddleware()
        {
            return _Middleware;
        }

        private static Compose.DelegateComposedDispatch _Middleware(MiddlewareAPI middlewareAPI)
        {
            return _ComposeDispatch;   
        }

        private static Store.DelegateDispatch _ComposeDispatch(Store.DelegateDispatch nextDispatch)
        {
            return (action) => {
                var storeAction = (StoreAction) action;
                Debug.Log("Logger: " + storeAction.Type);
                return nextDispatch(action);
            };
        }
    }
}