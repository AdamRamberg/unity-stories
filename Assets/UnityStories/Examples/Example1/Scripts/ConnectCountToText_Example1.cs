using UnityEngine;
using UnityStories;

public class ConnectCountToText_Example1 : MonoBehaviour 
{
    public StoriesHelper storiesHelper;
	public CountText_Example1 countText_Example1;

    void Start() 
    {
        storiesHelper.Setup(gameObject, MapStoriesToProps);
    }

	void MapStoriesToProps(Story story)
    {
        countText_Example1.SetCountText(story.Get<CountStory>().count);
        countText_Example1.SetCountTextNotPersisted(story.Get<CountStory>().countNotPresisted);
    }
}
