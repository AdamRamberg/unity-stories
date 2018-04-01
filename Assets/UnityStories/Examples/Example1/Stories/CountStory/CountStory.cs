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

    // Actions
    public class StoryActionIncrementCount : StoryAction 
    {
        public override void ApplyToStory(Story story) 
        {
            if (story.GetType() != typeof(CountStory)) return;

            var countStory = (CountStory) story;
            countStory.count++;
            countStory.countNotPresisted++;
        }
    }

    public static class StoryActionIncrementCountFactory
    {
        static StoryActionFactoryHelper<StoryActionIncrementCount> helper = new StoryActionFactoryHelper<StoryActionIncrementCount>();
        public static StoryActionIncrementCount Get() 
        {
            var action = helper.GetUnused();
            return action != null ? action : helper.CacheAndReturn(new StoryActionIncrementCount());
        }
    }

    public class StoryActionDecrementCount : StoryAction 
    {
        public override void ApplyToStory(Story story) 
        {
            if (story.GetType() != typeof(CountStory)) return;

            var countStory = (CountStory) story;
            countStory.count--;
            countStory.countNotPresisted--;
        }
    }

    public static class StoryActionDecrementCountFactory
    {
        static StoryActionFactoryHelper<StoryActionDecrementCount> helper = new StoryActionFactoryHelper<StoryActionDecrementCount>();
        public static StoryActionDecrementCount Get() 
        {
            var action = helper.GetUnused();
            return action != null ? action : helper.CacheAndReturn(new StoryActionDecrementCount());
        }
    }
}
