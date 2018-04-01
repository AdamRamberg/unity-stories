using UnityEngine;

namespace UnityStories 
{
	[CreateAssetMenu(menuName = "Unity Stories/Entry Story")]
	public class EntryStory : Story 
	{
		public override void InitStory()
		{
			InitStorySub(this);
		}

		private void InitStorySub(Story story)
		{
			if (story.subStories == null) return;
			
			for (var i = 0; i < story.subStories.Length; ++i) 
			{
				story.subStories[i].InitStory();
				InitStorySub(story.subStories[i]);
			}
		}
	}
}