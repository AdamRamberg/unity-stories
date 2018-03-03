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
			InitStorySub(this);
		}

		private void InitStorySub(Story story)
		{
			if (story.subStories == null) return;
			
			foreach(var subStory in story.subStories) 
			{
				subStory.InitStory();
				InitStorySub(subStory);
			}
		}
	}
}