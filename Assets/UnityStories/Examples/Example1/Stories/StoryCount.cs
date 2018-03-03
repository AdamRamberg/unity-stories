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

	public override void ActionHandler(StoryAction action)
    {
        switch (action.Type)
        {
            case Constants_Example1.INCREMENT_COUNTER:
                {
                    count++;
                    countNotPresisted++;
                    break;
                }
            case Constants_Example1.DECREMENT_COUNTER:
                {
                    count--;
                    countNotPresisted--;
                    break;
                }
        }
    }
}
