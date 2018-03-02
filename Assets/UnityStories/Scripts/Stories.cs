using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStories 
{
    [CreateAssetMenu(menuName = "UnityStories/Stories")]
	public class Stories : ScriptableObject
	{
		public StoriesCreator storiesCreator;

		public delegate StoryAction DelegateDispatch(StoryAction action);
		public delegate void DelegateConnect(Action<Story> handler);
		public delegate void DelegateListen(Action<StoryAction> handler);
		public delegate int DelegateGetConnectedCount();
        public delegate EntryStory DelegateGetState();

		private DelegateDispatch _dispatch;
		public DelegateDispatch Dispatch {
			get { return this._dispatch; }
			set { this._dispatch = value; }
		}

		private DelegateConnect _connect;
		public DelegateConnect Connect {
			get { return this._connect; }
			set { this._connect = value; }
		}

		private DelegateListen _listen;
		public DelegateListen Listen {
			get { return this._listen; }
			set { this._listen = value; }
		}

		private DelegateGetConnectedCount _getConnectedCount;
		public DelegateGetConnectedCount GetConnectedCount {
			get { return this._getConnectedCount; }
			set { this._getConnectedCount = value; }
		}

		private DelegateGetState _getState;
		public DelegateGetState GetState {
			get { return this._getState; }
			set { this._getState = value; }
		}

		public void CreateStories() 
		{
			if (storiesCreator != null) 
				storiesCreator.CreateStories(this);
		}
		
		private void OnEnable()
        {
			CreateStories();
		}
	}
}