using UnityEngine;
using UnityStories;

public class FollowMouse_Example2 : MonoBehaviour 
{
	public Stories stories;
	private Vector2 target;
	
	void Start () 
	{
		stories.Connect(MapStoriesToProps);
	}
	
	void Update () 
	{
		transform.position = Vector3.Lerp(transform.position, target, 6f * Time.deltaTime);
	}

	public void MapStoriesToProps(Story story)
    {
        SetTarget(story.Get<MouseInputStory>().mousePos);
    }

	private void SetTarget(Vector2 mousePos)
	{
		target = Camera.main.ScreenToWorldPoint(mousePos);
	}
}
