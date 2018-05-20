using UnityEngine;
using UnityStories;

[CreateAssetMenu(menuName = "Unity Stories/Example1/Stories/Count Story")]
public class CountStory : Story
{
    // Variables that you want to keep track of in your story.
	public int count = 0;
	public int countNotPresisted = 0;

    // Init your variables here that you don't want to be persisted between plays.
	public override void InitStory()
	{
		countNotPresisted = 0;
	}

    // Actions / factories
    public class IncrementCount : GenericAction<CountStory> 
    {
        public override void Action(CountStory story) 
        {
            story.count++;
            story.countNotPresisted++;
        }
    }
    public static GenericFactory<IncrementCount, CountStory> IncrementCountFactory = new GenericFactory<IncrementCount, CountStory>();

    public class DecrementCount : GenericAction<CountStory> 
    {
        public override void Action(CountStory story) 
        {
            story.count--;
            story.countNotPresisted--;
        }
    }
    public static GenericFactory<DecrementCount, CountStory> DecrementCountFactory = new GenericFactory<DecrementCount, CountStory>();
}
