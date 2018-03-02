using System.Collections.Generic;
using UnityStories;

public struct StoryActionIncrementCount : StoryAction 
{
	public string Type { get { return Constants_Example1.INCREMENT_COUNTER; } }
}

public struct StoryActionDecrementCount : StoryAction 
{
public string Type { get { return Constants_Example1.DECREMENT_COUNTER; } }
}
