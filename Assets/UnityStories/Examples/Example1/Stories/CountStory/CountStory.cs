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
    public class IncrementCount : StoryAction 
    {
        public override void ApplyToStory(Story story) 
        {
           if (!(story is CountStory)) return;

            var countStory = (CountStory) story;
            countStory.count++;
            countStory.countNotPresisted++;
        }
    }

    public static class IncrementCountFactory
    {
        static StoryActionFactoryHelper<IncrementCount> helper = new StoryActionFactoryHelper<IncrementCount>();
        public static IncrementCount Get() 
        {
            var action = helper.GetUnused();
            return action != null ? action : helper.CacheAndReturn(new IncrementCount());
        }
    }

    public class DecrementCount : StoryAction 
    {
        public override void ApplyToStory(Story story) 
        {
            if (!(story is CountStory)) return;

            var countStory = (CountStory) story;
            countStory.count--;
            countStory.countNotPresisted--;
        }
    }

    public static class DecrementCountFactory
    {
        static StoryActionFactoryHelper<DecrementCount> helper = new StoryActionFactoryHelper<DecrementCount>();
        public static DecrementCount Get() 
        {
            var action = helper.GetUnused();
            return action != null ? action : helper.CacheAndReturn(new DecrementCount());
        }
    }
}
