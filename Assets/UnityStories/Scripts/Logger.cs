using UnityEngine;

namespace UnityStories 
{
    [CreateAssetMenu(menuName = "Unity Stories/Middleware/Logger")]
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

        private static Stories.DelegateDispatch _ComposeDispatch(Stories.DelegateDispatch nextDispatch)
        {
            return (action) => {
                var storeAction = (StoryAction) action;
                Debug.Log("Logger: " + storeAction.Type);
                return nextDispatch(action);
            };
        }
    }
}