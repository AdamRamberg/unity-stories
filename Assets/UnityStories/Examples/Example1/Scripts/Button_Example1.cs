using UnityEngine;
using UnityStories;

public class Button_Example1 : MonoBehaviour
{
    public StoriesHelper storiesHelper;

    public void OnClick_Inc()
    {
        storiesHelper.Dispatch(CountStory.IncrementCountFactory.Get());
    }

    public void OnClick_Dec()
    {
        storiesHelper.Dispatch(CountStory.DecrementCountFactory.Get());
    }
}
