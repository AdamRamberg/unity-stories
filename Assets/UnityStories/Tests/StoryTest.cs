namespace UnityStories 
{
	// Story used for testing
	public class StoryTest : Story 
	{
		public override string Name { get { return "test"; } }

		public bool updated = false;
		public bool wasHandlerCalled = false;

		public override void ActionHandler(StoryAction action)
		{
			wasHandlerCalled = true;

			switch (action.Type)
			{
				case "test":
					{
						updated = true;
						break;
					}
			}
		}
	}
}