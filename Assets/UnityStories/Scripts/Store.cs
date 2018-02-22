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

		private void OnEnable()
        {
			if (state != null) state.InitState();
		}

		public void Dispatch(StoreAction action)
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

        public void Connect(Action<State> handler)
        {
            mapStateToPropsHandlers.Add(handler);
            // Send state on connect
            handler(state);
        }

		public void Listen(Action<StoreAction> handler)
		{
			actionListeners.Add(handler);
		}

		public int MapStateToPropsHandlerCount { get { return mapStateToPropsHandlers.Count; } }
	}
}