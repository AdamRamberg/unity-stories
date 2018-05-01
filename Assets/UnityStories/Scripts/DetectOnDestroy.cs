using System;
using UnityEngine;

namespace UnityStories
{
	public class DetectOnDestroy : MonoBehaviour 
	{
		private Action onDestroyAction;
		public Action OnDestroyAction
		{
			get { return onDestroyAction; }
			set { onDestroyAction = value; }
		}
		void OnDestroy () 
		{
			if (onDestroyAction != null)
			{
				onDestroyAction.Invoke();
			}
		}
	}
}