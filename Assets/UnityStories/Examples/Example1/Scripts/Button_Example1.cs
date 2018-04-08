using UnityEngine;
using UnityStories;

public class Button_Example1 : MonoBehaviour
{
    public Stories stories;

    public void OnClick_Inc()
    {
        stories.Dispatch(CountStory.IncrementCountFactory.Get());
    }

    public void OnClick_Dec()
    {
        stories.Dispatch(CountStory.DecrementCountFactory.Get());
    }
}
