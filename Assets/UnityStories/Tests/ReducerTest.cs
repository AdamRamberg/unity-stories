namespace UnityStories 
{
	// Reducer used to test Store
	public class ReducerTest : Reducer
	{
		public override string Name { get { return "test"; } }

		public bool wasHandlerCalled = false;

		public override void Handler(Story story, StoryAction action)
		{
			if (!(story is StoryTest)) return;
			var storyTest = (StoryTest) story;
			wasHandlerCalled = true;

			switch (action.Type)
			{
				case "test":
					{
						storyTest.updated = true;
						break;
					}
			}
		}
	}
}