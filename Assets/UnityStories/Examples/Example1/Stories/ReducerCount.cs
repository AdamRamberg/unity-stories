using UnityStories;
using UnityEngine;

[CreateAssetMenu(menuName = "UnityStories/Example1/Reducers/ReducerCount")]
public class ReducerCount : Reducer
{
    public override string Name { get { return StoryCount.NAME; } }

    public override void Handler(Story story, StoryAction action)
    {
        if (!(story is StoryCount)) return;
        var storyCount = (StoryCount) story;

        switch (action.Type)
        {
            case Constants_Example1.INCREMENT_COUNTER:
                {
                    storyCount.count++;
                    storyCount.countNotPresisted++;
                    break;
                }
            case Constants_Example1.DECREMENT_COUNTER:
                {
                    storyCount.count--;
                    storyCount.countNotPresisted--;
                    break;
                }
        }
    }
}
