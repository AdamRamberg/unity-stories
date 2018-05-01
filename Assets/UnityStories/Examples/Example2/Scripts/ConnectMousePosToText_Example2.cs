using UnityEngine;
using UnityStories;

public class ConnectMousePosToText_Example2 : MonoBehaviour 
{
	public StoriesHelper storiesHelper;
	public MousePosText_Example2 mousePosText_Example2;
	
    void Start() 
    {
        storiesHelper.Setup(gameObject, MapStoriesToProps);
    }
	
	public void MapStoriesToProps(Story story)
    {
        mousePosText_Example2.SetMousePosText(story.Get<MouseInputStory>().mousePos);
    }
}
