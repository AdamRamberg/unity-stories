using UnityEngine;

namespace UnityStories 
{
    [CreateAssetMenu(menuName = "UnityStories/Middleware/Logger")]
    public abstract class Logger : Middleware
    {
        public override DelegateMiddleware _ReturnMiddleware()
        {
            // START HERE...
            return null;
        }
    }
}