using UnityEngine;
using UnityStories;

[CreateAssetMenu(menuName = "Unity Stories/Example1/Stories/Count Story")]
public class CountStory : Story 
{
    // Define the name of the Story. Should be unique for all of your Stories.
	public static string NAME = "count";
	public override string Name { get { return NAME; } }

    // Variables that you want to keep track of in your story.
	public int count = 0;
	public int countNotPresisted = 0;

    // Init your variables here that you don't want to be persisted between plays.
	public override void InitStory()
	{
		countNotPresisted = 0;
	}

    // Handle Story Actions and mutate your state accordingly.
	public override void ActionHandler(StoryAction action)
    {
        switch (action.Type)
        {
            case INCREMENT_COUNTER:
                {
                    count++;
                    countNotPresisted++;
                    break;
                }
            case DECREMENT_COUNTER:
                {
                    count--;
                    countNotPresisted--;
                    break;
                }
        }
    }

    // Action constants
    public const string INCREMENT_COUNTER = "INCREMENT_COUNTER";
    public const string DECREMENT_COUNTER = "DECREMENT_COUNTER";

    // Actions
    public struct StoryActionIncrementCount : StoryAction 
    {
        public string Type { get { return INCREMENT_COUNTER; } }
    }

    public struct StoryActionDecrementCount : StoryAction 
    {
        public string Type { get { return DECREMENT_COUNTER; } }
    }
}
