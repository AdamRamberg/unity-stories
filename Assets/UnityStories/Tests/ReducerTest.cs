namespace UnityStories 
{
	// Reducer used to test Store
	public class ReducerTest : Reducer
	{
		public override string Name { get { return "test"; } }

		public bool wasHandlerCalled = false;

		public override void Handler(State state, StoreAction action)
		{
			if (!(state is StateTest)) return;
			var stateTest = (StateTest) state;
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