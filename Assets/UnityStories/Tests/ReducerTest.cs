namespace UnityStories 
{
	// Reducer used to test Store
	public class ReducerTest : Reducer
	{
		public override string Name { get { return "test"; } }

		public bool wasHandlerCalled = false;

		public override void Handler(Story state, StoryAction action)
		{
			if (!(state is StoryTest)) return;
			var stateTest = (StoryTest) state;
			wasHandlerCalled = true;

			switch (action.Type)
			{
				case "test":
					{
						stateTest.updated = true;
						break;
					}
			}
		}
	}
}