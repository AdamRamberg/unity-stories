using UnityStories;
using UnityEngine;

[CreateAssetMenu(menuName = "UnityStories/Example1/Reducers/ReducerCount")]
public class ReducerCount : Reducer
{
    public override string Name { get { return StateCount.NAME; } }

    public override void Handler(State state, StoreAction action)
    {
        if (!(state is StateCount)) return;
        var stateCount = (StateCount) state;

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
