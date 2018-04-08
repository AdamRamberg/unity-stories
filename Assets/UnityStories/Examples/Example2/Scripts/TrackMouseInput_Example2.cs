using UnityEngine;
using UnityStories;
public class TrackMouseInput_Example2 : MonoBehaviour 
{
	public Stories stories;

	void Update () 
	{
		stories.Dispatch(MouseInputStory.UpdateMousePosFactory.Get(Input.mousePosition));
	}
}
