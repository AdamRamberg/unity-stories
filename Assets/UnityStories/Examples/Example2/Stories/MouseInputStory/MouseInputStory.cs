using UnityEngine;
using UnityStories;

[CreateAssetMenu(menuName = "Unity Stories/Example2/Stories/Mouse Input Story")]
public class MouseInputStory : Story
{
    // Variables that you want to keep track of in your story.
	public Vector2 mousePos = Vector2.zero;

    // Init your variables here that you don't want to be persisted between plays.
	public override void InitStory()
	{
		mousePos = Vector2.zero;
	}

    // Actions / factories
    public class UpdateMousePos : GenericAction<MouseInputStory, Vector2> 
    {
        public override void Action(MouseInputStory story, Vector2 mousePos) 
        {
            story.mousePos = mousePos;
        }
    }
    public static GenericFactory<UpdateMousePos, MouseInputStory, Vector2> UpdateMousePosFactory = new GenericFactory<UpdateMousePos, MouseInputStory, Vector2>();
}
