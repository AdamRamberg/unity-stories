using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStories 
{
    [CreateAssetMenu(menuName = "UnityStories/Store")]
	public class Store : ScriptableObject
	{
		public StoreCreator storeCreator;

		public delegate StoreAction DelegateDispatch(StoreAction action);
		public delegate void DelegateConnect(Action<State> handler);
		public delegate void DelegateListen(Action<StoreAction> handler);
		public delegate int DelegateGetConnectedCount();
        public delegate MainState DelegateGetState();

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

		public void CreateStore() 
		{
			storeCreator.CreateStories(this);
		}
		
		private void OnEnable()
        {
			CreateStore();
		}
	}
}