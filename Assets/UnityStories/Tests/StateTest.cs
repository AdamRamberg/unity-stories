namespace UnityStories 
{
	// State used to test Store
	public class StateTest : State 
	{
		public override string Name { get { return "test"; } }

		public bool updated = false;
	}
}