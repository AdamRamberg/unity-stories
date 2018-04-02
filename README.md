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

Create a Stories object (Assets/Create/Unity Stories/Stories) and an Entry Story (Press "Create Entry Story" button on Stories asset or Assets/Create/Unity Stories/Entry Story). Drag and drop the Entry Story to the Stories object.

Create your stories (state containers) by inheriting from the abstract Story class and connect them to the Entry Story. Here is an example of a simple story with two int variables, one that is persistied between plays and one that is initalized each time we start the game: 
```
[CreateAssetMenu(menuName = "Unity Stories/Example1/Stories/Count Story")]
public class CountStory : Story
{
    // Variables that you want to keep track of in your story.
	public int count = 0;
	public int countNotPresisted = 0;

    // Init your variables here that you don't want to be persisted between plays.
	public override void InitStory()
	{
		countNotPresisted = 0;
	}

    // Actions / factories
    public class IncrementCounter : StoryAction 
    {
        public override void ApplyToStory(Story story) 
        {
           if (!(story is CountStory)) return;

            var countStory = (CountStory) story;
            countStory.count++;
            countStory.countNotPresisted++;
        }
    }

    public static class IncrementCountFactory
    {
        static StoryActionFactoryHelper<IncrementCounter> helper = new StoryActionFactoryHelper<IncrementCounter>();
        public static IncrementCounter Get() 
        {
            var action = helper.GetUnused();
            return action != null ? action : helper.CacheAndReturn(new IncrementCounter());
        }
    }

    public class DecrementCount : StoryAction 
    {
        public override void ApplyToStory(Story story) 
        {
            if (!(story is CountStory)) return;

            var countStory = (CountStory) story;
            countStory.count--;
            countStory.countNotPresisted--;
        }
    }

    public static class DecrementCountFactory
    {
        static StoryActionFactoryHelper<DecrementCount> helper = new StoryActionFactoryHelper<DecrementCount>();
        public static DecrementCount Get() 
        {
            var action = helper.GetUnused();
            return action != null ? action : helper.CacheAndReturn(new DecrementCount());
        }
    }
}
```

There might seems to be a lot going on in this Story. Below is a breakdown on what everything is: 
- First we define the variables that we want to keep track of in this Story. This is what the Story is all about. You can store any data or object that you want to keep track of and change when StoryActions are dispatched. 
- The InitStory() method is used if you want to initalize variables each play. 
- Lastly we define our StoryActions and corrsponding factories (these can be defined in a seperate file if that is preferable). These can be dispatched from your code in order to change the state in our Story. The StoryAction can contain data and is responsible to define how it changes our Story's data. In the above example we define 2 actions (and corrsponding factories) that increments and decrements our variables stored in the Story.

*One major difference from Redux is that we don't define a reducer. Instead we let StoryActions define how we change our Story / state. Another major difference is that a StoryAction actual mutates our Story / state. This is because Unity Stories tries to minimize the amount of garbage being generated.*

When the Story is defined you can now use is it in your code. Here is an example of how you would dispatch a StoryAction (using our defined factories) from a button click: 
```
public class Button : MonoBehaviour
{
    public Stories stories;

    public void OnClick_Inc()
    {
        stories.Dispatch(CountStory.IncrementCountFactory.Get());
    }

    public void OnClick_Dec()
    {
        stories.Dispatch(CountStory.DecrementCountFactory.Get());
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
        count = story.Get<CountStory>().count;
        countNotPersisted = story.Get<CountStory>().countNotPresisted;
    }
}
```

See more examples of how to use Unity Stories in the Examples folder. 

## Middleware
Unity Stories is allowing users to use and define enhancers (like Redux allows users to enhance their store). Unity Stories ships with one enhancer creator, ApplyMiddleware, that is making it possible to apply middleware to the dispatch method. Logger is a middleware defined in Unity Stories that shows the API and a simple example of how a middleware can be defined. 

## Performance
First of all, it is strongly recommended to create StoryAction factories in order to minimize garbage. Furthermore, in order to avoid unnecessary garbage collection reference types (for example strings) in StoryActions should be avoided if used often (for example in the Update loop) when possible.