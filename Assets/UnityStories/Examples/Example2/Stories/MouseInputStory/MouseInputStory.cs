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
    public class UpdateMousePos : StoryAction 
    {
        public Vector2 updateMousePos;

        public UpdateMousePos(Vector2 updateMousePos) 
        {
            this.updateMousePos = updateMousePos;
        }
        public override void ApplyToStory(Story story) 
        {
           if (!(story is MouseInputStory)) return;

            var mouseInputStory = (MouseInputStory) story;
            mouseInputStory.mousePos = updateMousePos;
        }
    }

    public static class UpdateMousePosFactory
    {
        static StoryActionFactoryHelper<UpdateMousePos> helper = new StoryActionFactoryHelper<UpdateMousePos>();
        public static UpdateMousePos Get(Vector2 updateMousePos) 
        {
            var action = helper.GetUnused();
            if(action != null)
            {
                action.updateMousePos = updateMousePos;
                return action;
            }
            return helper.CacheAndReturn(new UpdateMousePos(updateMousePos));
        }
    }
}
