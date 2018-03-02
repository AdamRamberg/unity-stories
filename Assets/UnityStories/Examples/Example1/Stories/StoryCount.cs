using UnityEngine;
using UnityStories;

[CreateAssetMenu(menuName = "UnityStories/Example1/Stories/StoryCount")]
public class StoryCount : Story 
{
	public static string NAME = "count";
	public override string Name { get { return NAME; } }

	public override void InitStory()
	{
		countNotPresisted = 0;
	}

	public int count = 0;
	public int countNotPresisted = 0;

	
}
