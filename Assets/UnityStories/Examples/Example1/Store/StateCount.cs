using UnityEngine;
using UnityStories;

[CreateAssetMenu(menuName = "UnityStories/Example1/States/StateCount")]
public class StateCount : State 
{
	public static string NAME = "count";
	public override string Name { get { return NAME; } }

	public override void InitState()
	{
		countNotPresisted = 0;
	}

	public int count = 0;
	public int countNotPresisted = 0;

	
}
