using UnityEngine;
using UnityEngine.UI;
using UnityStories;

public class CountText_Example1 : MonoBehaviour 
{
    public Text countText;
    public Text countNotPersistedText;
    public Stories stories;

    void Start() 
    {
        stories.Connect(MapStoriesToProps);
    }

    void SetCountText(int count)
    {
        countText.text = "Count is: " + count;
    }

    void SetCountTextNotPersisted(int count)
    {
        countNotPersistedText.text = "Not persisted count is: " + count;
    }

    public void MapStoriesToProps(Story story)
    {
        SetCountText(story.Get<CountStory>().count);
        SetCountTextNotPersisted(story.Get<CountStory>().countNotPresisted);
    }
}
