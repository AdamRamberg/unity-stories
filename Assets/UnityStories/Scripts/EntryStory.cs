using UnityEngine;

namespace UnityStories 
{
	[CreateAssetMenu(menuName = "UnityStories/EntryStory")]
	public class EntryStory : Story 
	{
		private const string NAME = "main";
		public override string Name { get { return NAME; } }

		public override void InitStory()
		{
			InitStateSub(this);
		}

		private void InitStateSub(Story state)
		{
			foreach(var subState in state.subStories) 
			{
				subState.InitStory();
				InitStateSub(subState);
			}
		}
	}
}