using UnityEngine;

namespace UnityStories 
{
	[CreateAssetMenu(menuName = "UnityStories/MainState")]
	public class MainState : State 
	{
		private const string NAME = "main";
		public override string Name { get { return NAME; } }

		public override void InitState()
		{
			InitStateSub(this);
		}

		private void InitStateSub(State state)
		{
			foreach(var subState in state.subStates) 
			{
				subState.InitState();
				InitStateSub(subState);
			}
		}
	}
}