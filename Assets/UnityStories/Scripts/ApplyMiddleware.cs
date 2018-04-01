using UnityEngine;

namespace UnityStories 
{
    [CreateAssetMenu(menuName = "Unity Stories/Apply Middleware")]
    public class ApplyMiddleware : EnhancerCreator
    {
        public Middleware[] middlewares;
        public override Enhancer CreateEnhancer()
        {
            var delegateMiddlewares = middlewares.Length > 0 ? new Middleware.DelegateMiddleware[middlewares.Length] : null;

            if (delegateMiddlewares != null) 
            {
                for(var i = 0; i < delegateMiddlewares.Length; ++i)
                {
                    delegateMiddlewares[i] = middlewares[i].ReturnMiddleware();
                }
            }

            return _CreateEnhancer(delegateMiddlewares);
        }

        private Enhancer _CreateEnhancer(Middleware.DelegateMiddleware[] middlewares) 
        {
            return (createStore) => (store, enhancer) => 
            {
                createStore(store);
                if (middlewares == null || middlewares.Length == 0) return;

                var _dispatch = store.Dispatch;
                var chain = new Compose.DelegateComposedDispatch[middlewares.Length];

                var middlewareAPI = new Middleware.MiddlewareAPI () {
                    GetStories = store.GetStories,
                    Dispatch = (action) => {
                        return _dispatch (action);
                    }
			    };

                for (var i = 0; i < middlewares.Length; ++i) 
                {
				    chain[i] = middlewares[i](middlewareAPI);
			    }
			    _dispatch = Compose.ComposeDispatches(chain)(store.Dispatch);
			    store.Dispatch = _dispatch;                
            };
        }
    }
}