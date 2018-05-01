using UnityEngine;
using UnityStories;
public class TrackMouseInput_Example2 : MonoBehaviour 
{
	public StoriesHelper storiesHelper;

	void Update () 
	{
		storiesHelper.Dispatch(MouseInputStory.UpdateMousePosFactory.Get(Input.mousePosition));
	}
}
