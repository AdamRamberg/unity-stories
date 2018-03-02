using UnityStories;
using UnityEngine;

[CreateAssetMenu(menuName = "UnityStories/Example1/Reducers/ReducerCount")]
public class ReducerCount : Reducer
{
    public override string Name { get { return StoryCount.NAME; } }

    public override void Handler(Story state, StoryAction action)
    {
        if (!(state is StoryCount)) return;
        var stateCount = (StoryCount) state;

        switch (action.Type)
        {
            case Constants_Example1.INCREMENT_COUNTER:
                {
                    stateCount.count++;
                    stateCount.countNotPresisted++;
                    break;
                }
            case Constants_Example1.DECREMENT_COUNTER:
                {
                    stateCount.count--;
                    stateCount.countNotPresisted--;
                    break;
                }
        }
    }
}
