using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStories 
{
    [Serializable]
    public class StoreCreator 
    {
        public Reducer[] reducers;
        public MainState state;
		public EnhancerCreator enhancerCreators;
        private List<Action<State>> mapStateToPropsHandlers = new List<Action<State>>();
        private List<Action<StoreAction>> actionListeners = new List<Action<StoreAction>>();

		public delegate void DelegateCreateStore(Store store, EnhancerCreator.Enhancer enhancer = null);

		public StoreCreator() 
		{
			CreateStore = _CreateStore;
		}

		public void CreateStories(Store store) 
		{
			_CreateStore(store, enhancerCreators.CreateEnhancer());
		}
		
        private DelegateCreateStore CreateStore;
		private void _CreateStore(Store store, EnhancerCreator.Enhancer enhancer = null)
		{
			if (enhancer != null) 
			{
				enhancer(_CreateStore)(store);
				return;
			}

			store.Dispatch = DefImpl_Dispatch;
			store.Connect = DefImpl_Connect;
			store.Listen = DefImpl_Listen;
			store.GetConnectedCount = DefImpl_GetConnectedCount;
            store.GetState = DefImpl_GetState;

			if (state != null) state.InitState();
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

        public MainState DefImpl_GetState()
        {
            return state;
        }
    }
}