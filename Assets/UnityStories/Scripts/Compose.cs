using UnityEngine;

namespace UnityStories
{
	public static class Compose 
	{
		public delegate Stories.DelegateDispatch DelegateComposedDispatch(Stories.DelegateDispatch nextDispatch);
		public delegate DelegateComposedDispatch DelegateComposeDispatches(DelegateComposedDispatch[] composedDispatches);
		public static DelegateComposeDispatches ComposeDispatches = _ComposeDispatches;

		private static DelegateComposedDispatch _ComposeDispatches(DelegateComposedDispatch[] composedDispatches) 
		{
			if (composedDispatches.Length == 0) 
			{
				return null;
			}

			if (composedDispatches.Length == 1) 
			{
				return composedDispatches[0];
			}

			var lastComposed = composedDispatches[composedDispatches.Length - 1];
			return nextDispatch => {
				var composed = lastComposed(nextDispatch);
				for (var i = composedDispatches.Length - 2; i >= 0; --i) 
				{
					composed = composedDispatches[i](composed);
				}
				return composed;
			};
		}

		public delegate EnhancerCreator.Enhancer DelegateComposeEnhancers(EnhancerCreator.Enhancer[] enhancers);
		public static DelegateComposeEnhancers ComposeEnhancers = _ComposeEnhancers;

		private static EnhancerCreator.Enhancer _ComposeEnhancers(EnhancerCreator.Enhancer[] enhancers)
		{
			if (enhancers.Length == 0) 
			{
				return null;
			}

			if (enhancers.Length == 1) 
			{
				return enhancers[0];
			}

			var lastComposed = enhancers[enhancers.Length - 1];
			return nextEnhancer => {
				var composed = lastComposed(nextEnhancer);
				for (var i = enhancers.Length - 2; i >= 0; --i) 
				{
					composed = enhancers[i](composed);
				}
				return composed;
			};
		}
	}
}