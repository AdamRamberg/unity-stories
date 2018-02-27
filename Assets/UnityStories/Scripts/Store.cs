using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStories 
{
    [CreateAssetMenu(menuName = "UnityStories/Store")]
	public class Store : ScriptableObject
	{
		public Reducer[] reducers;
		public MainState state;
		private List<Action<State>> mapStateToPropsHandlers = new List<Action<State>>();
		private List<Action<StoreAction>> actionListeners = new List<Action<StoreAction>>();

		public delegate StoreAction DelegateDispatch(StoreAction action);
		public delegate void DelegateConnect(Action<State> handler);
		public delegate void DelegateListen(Action<StoreAction> handler);
		public delegate int DelegateGetConnectedCount();

		private DelegateDispatch _dispatch;
		public DelegateDispatch Dispatch {
			get { return this._dispatch; }
			internal set { this._dispatch = value; }
		}

		private DelegateConnect _connect;
		public DelegateConnect Connect {
			get { return this._connect; }
			internal set { this._connect = value; }
		}

		private DelegateListen _listen;
		public DelegateListen Listen {
			get { return this._listen; }
			internal set { this._listen = value; }
		}

		private DelegateGetConnectedCount _getConnectedCount;
		public DelegateGetConnectedCount GetConnectedCount {
			get { return this._getConnectedCount; }
			internal set { this._getConnectedCount = value; }
		}

		private void CreateStore()
		{
			this._dispatch = DefImpl_Dispatch;
			this._connect = DefImpl_Connect;
			this._listen = DefImpl_Listen;
			this._getConnectedCount = DefImpl_GetConnectedCount;

			if (state != null) state.InitState();
		}

		private void OnEnable()
        {
			CreateStore();
		}

		private StoreAction DefImpl_Dispatch(StoreAction action)
        {
			// Send action to reducers
			foreach (var subState in state.subStates)
			{
				SendToReducers(action, subState, reducers);
			}

            // Send update to everyone that have mapped their props
            foreach (var handler in mapStateToPropsHandlers)
            {
                handler(state);
            }

            // Send action forward to listeners
            foreach (var handler in actionListeners)
            {
                handler(action);
            }

			#if UNITY_EDITOR
            RepaintEditor();
			#endif

			return action;
        }

		private void SendToReducers(StoreAction action, State state, Reducer[] reducers)
		{
			foreach (var reducer in reducers)
            {
				if (state.Name == reducer.Name) 
				{
					reducer.Handler(state, action);

					if (state.subStates != null)
					{
						foreach (var subState in state.subStates) 
						{
							SendToReducers(action, subState, reducer.subReducers);
						}
					}
				}
            }
		}

		#if UNITY_EDITOR
		// Repaint unity editor
		public delegate void RepaintEditorAction();
		public event RepaintEditorAction WantRepaintEditor;
		private void RepaintEditor()
		{
			if (WantRepaintEditor != null)
			{
				WantRepaintEditor();
			}
		}
		#endif

        public void DefImpl_Connect(Action<State> handler)
        {
            mapStateToPropsHandlers.Add(handler);
            // Send state on connect
            handler(state);
        }

		public int DefImpl_GetConnectedCount() { return mapStateToPropsHandlers.Count; }

		public void DefImpl_Listen(Action<StoreAction> handler)
		{
			actionListeners.Add(handler);
		}
	}
}