using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStories;

public class Button : MonoBehaviour
{
    public Stories stories;

    public void OnClick_Inc()
    {
        stories.Dispatch(new CountStory.StoryActionIncrementCount());
    }

    public void OnClick_Dec()
    {
        stories.Dispatch(new CountStory.StoryActionDecrementCount());
    }
}
