# <img src="https://s3.amazonaws.com/unity-stories/unity-stories-withname.png" style="max-height: 128px !important; min-height: 96px !important;">
Unity Stories is a state container for games built in Unity utilizing Scriptable Objects. 

## Influences
Unity Stories is mainly taking inspiration and is influenced by <a href="https://github.com/reactjs/redux">Redux</a> and <a href="https://github.com/facebook/flux">Flux</a>. 

Forerunner implementations in Unity / C# that also have influenced Unity Stories are: 
- <a href="https://github.com/gblue1223/redux-unity3d">redux-unity-3d</a>
- <a href="https://github.com/mattak/Unidux">Unidux</a>

## Motivation
The general approach to building scripts in Unity often generates a code base that is monolithic. This results in that your code is cumbersome to test, non-modular and hard to debug and understand. 

The aim of Unity Stories is to seperate concerns between your game state and the implementation of your game logic making your scripts modular. The will make protyping your game faster and makes it easier to make changes to your code base even though your project has grown large.  

## Installation
Import unitypackage from latest releases or download and import into your project from the Unity Asset Store (coming soon).

## Usage
In order to utilize this library you should understand how flux and redux works. See links above. 

Create a Stories object (Assets/Create/Unity Stories/Stories) and an Entry Story (Assets/Create/Unity Stories/Entry Story). Drag and drop the Entry Story to the Stories object.

Create your stories (state containers) by inheriting from the abstract Story class and connect them to the Entry Story. Here is an example of a simple story with two int variables, one that is persistied between plays and one that is initalized each time we start the game: 
```
public class CountStory : Story 
{
    // Define the name of the Story. Should be unique for all of your Stories.
	public static string NAME = "count";
	public override string Name { get { return NAME; } }

    // Variables that you want to keep track of in your story.
	public int count = 0;
	public int countNotPresisted = 0;

    // Init your variables here that you don't want to be persisted between plays.
	public override void InitStory()
	{
		countNotPresisted = 0;
	}

    // Handle Story Actions and mutate your state accordingly.
	public override void ActionHandler(StoryAction action)
    {
        switch (action.Type)
        {
            case INCREMENT_COUNTER:
                {
                    count++;
                    countNotPresisted++;
                    break;
                }
            case DECREMENT_COUNTER:
                {
                    count--;
                    countNotPresisted--;
                    break;
                }
        }
    }

    // Action constants
    public const string INCREMENT_COUNTER = "INCREMENT_COUNTER";
    public const string DECREMENT_COUNTER = "DECREMENT_COUNTER";

    // Actions
    public struct StoryActionIncrementCount : StoryAction 
    {
        public string Type { get { return INCREMENT_COUNTER; } }
    }

    public struct StoryActionDecrementCount : StoryAction 
    {
        public string Type { get { return DECREMENT_COUNTER; } }
    }
}
```

There might seems to be a lot going on in this Story. Below is a breakdown on what everything is: 
- The first thing that in the Story is straight forward. It is a definition of the Story's name, which should be unique for all of your Stories.
- Next we define the variables that we want to keep track of in this Story. This is what the Story is all about. You can store any data or object that you want to keep track of and change when Story Actions are dispatched. 
- The InitStory() method is used if you want to initalize variables each play. 
- A Story is responsible of keeping track of one part of your game state, but also handling relevant incoming Story Actions (ActionHandler). In the above example the ActionHandler mutates the state when the Story Action INCREMENT_COUNTER and DECREMENT_COUNTER are dispatched. A crucial difference between Redux and Unity Stories is that an ActionHandler (reducer in Redux) is mutating the current state instead of returning a completley new state. This is because Unity Stories tries to minimize the amount of garbage being generated.
- The last thing we do in this Story is to define the Story Actions. These can be used and dispatched from your code when you want to manipulate this Story. 

When the Story is defined you can now use is it in your code. Here is an example of how you would dispatch a Story Action from a button click: 
```
public class Button : MonoBehaviour
{
    public Stories stories;

    public void OnClick_Inc()
    {
        stories.Dispatch(new CountStory.StoryActionIncrementCount());
    }

    public void OnClick_Dec()
    {
        stories.Dispatch(new CountStory.StoryActionDecrementCount());
    }
}
```

You can now use the values in this Story by connecting to your Stories from another script. Here is an example of displaying the values in an UI text element: 
```
public class CountText_Example1 : MonoBehaviour 
{
    public Text countText;
    public Text countNotPersistedText;
    private int count = 0;
    private int countNotPersisted = 0;
    public Stories stories;

    void Start() 
    {
        stories.Connect(MapStoriesToProps);
    }

    void Update()
    {
        // Bad practice to set text in Update() due to garbage collection. Only for demonstration purposes. 
        countText.text = "Count is: " + count;
        countNotPersistedText.text = "Not persisted count is: " + countNotPersisted;
    }

    public void MapStoriesToProps(Story story)
    {
        count = story.Get<CountStory>(CountStory.NAME).count;
        countNotPersisted = story.Get<CountStory>(CountStory.NAME).countNotPresisted;
    }
}
```

See more examples of how to use Unity Stories in the Examples folder. 

## Middleware
Unity Stories is allowing users to use enhance your Stories(like Redux allows users to enhance their store). Unity Stories ships with one enhancer creator, ApplyMiddleware, that is making it possible to apply middleware to the dispatch method. Logger is a middleware defined in Unity Stories that shows the API and a simple example of how a middleware can be defined. 

## Performance
In order to avoid unnecessary garbage collection reference types (for example strings) in stories and in actions should be avoided when possible. Furthermore, it also might be a good idea to use action factories in order to not generate unnecessary garbage. 