using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStories;

public class CountText_Example1 : MonoBehaviour {
    public Text countText;
    public Text countNotPersistedText;
    private int count = 0;
    private int countNotPersisted = 0;
    public Stories stories;

    void Start() 
    {
        stories.Connect(MapStateToProps);
    }

    void Update()
    {
        // Bad practice to set text in Update() due to garbage collection. Only for demonstration purposes. 
        countText.text = "Count is: " + count;
        countNotPersistedText.text = "Not persisted count is: " + countNotPersisted;
    }

    public void MapStateToProps(Story state)
    {
        count = state.Get<StoryCount>(StoryCount.NAME).count;
        countNotPersisted = state.Get<StoryCount>(StoryCount.NAME).countNotPresisted;
    }
}
