using UnityEngine;
using UnityEngine.UI;
using UnityStories;

public class MousePosText_Example2 : MonoBehaviour 
{
	public Text mousePosText;
	public Stories stories;

    void Start() 
    {
        stories.Connect(MapStoriesToProps);
    }

	void SetMousePosText(Vector2 mousePos)
    {
        mousePosText.text = "Mouse pos: " + mousePos;
    }

	public void MapStoriesToProps(Story story)
    {
        SetMousePosText(story.Get<MouseInputStory>().mousePos);
    }
}
