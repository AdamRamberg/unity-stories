using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStories;

public class Button : MonoBehaviour
{
    public Stories stories;

    public void OnClick_Inc()
    {
        stories.Dispatch(CountStory.StoryActionIncrementCountFactory.Get());
    }

    public void OnClick_Dec()
    {
        stories.Dispatch(CountStory.StoryActionDecrementCountFactory.Get());
    }
}
