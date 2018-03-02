using UnityEngine;
namespace UnityStories
{
	public static class Compose 
	{
		public delegate Stories.DelegateDispatch DelegateComposedDispatch(Stories.DelegateDispatch nextDispatch);
		public delegate DelegateComposedDispatch DelegateComposeDispatches(DelegateComposedDispatch[] composedDispatches);

		public static DelegateComposeDispatches ComposeDispatches = _ComposeDispatches;
		private static DelegateComposedDispatch _ComposeDispatches(DelegateComposedDispatch[] composedDispatches) {
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
	}
}