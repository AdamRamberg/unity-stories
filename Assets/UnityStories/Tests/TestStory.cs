namespace UnityStories 
{
	// Story used for testing
	public class TestStory : Story 
	{
		public bool updated = false;

		public class TestAction : StoryAction
		{
			public override void ApplyToStory(Story story)
			{
				if (story.GetType() != typeof(TestStory)) return;
				var testStory = (TestStory) story;
				testStory.updated = true;
			}
		}
	}
}