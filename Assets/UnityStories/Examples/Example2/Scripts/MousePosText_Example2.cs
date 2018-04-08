using UnityEngine;
using UnityEngine.UI;

public class MousePosText_Example2 : MonoBehaviour 
{
	public Text mousePosText;

	public void SetMousePosText(Vector2 mousePos)
    {
        mousePosText.text = "Mouse pos: " + mousePos;
    }
}
