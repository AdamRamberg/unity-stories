using System.Collections.Generic;
using UnityStories;

public struct ActionIncrementCount : StoreAction 
{
	public string Type { get { return Constants_Example1.INCREMENT_COUNTER; } }
}

public struct ActionDecrementCount : StoreAction 
{
public string Type { get { return Constants_Example1.DECREMENT_COUNTER; } }
}
