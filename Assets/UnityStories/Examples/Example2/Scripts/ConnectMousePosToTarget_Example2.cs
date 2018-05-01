using UnityEngine;
using UnityStories;

public class ConnectMousePosToTarget_Example2 : MonoBehaviour 
{
	public StoriesHelper storiesHelper;
	public FollowTarget_Example2 followTarget_Example2;	

	void Start () 
	{
		storiesHelper.Setup(gameObject, MapStoriesToProps);
	}
	
	public void MapStoriesToProps(Story story)
    {
        followTarget_Example2.SetTarget(story.Get<MouseInputStory>().mousePos);
    }
}
