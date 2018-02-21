using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStories;

public class CountText_Example1 : MonoBehaviour {
    public Text countText;
    public Text countNotPersistedText;
    private int count = 0;
    private int countNotPersisted = 0;
    public Store store;

    void Start() 
    {
        store.Connect(MapStateToProps);
    }

    void Update()
    {
        // Bad practice to set text in Update() due to garbage collection. Only for demonstration purposes. 
        countText.text = "Count is: " + count;
        countNotPersistedText.text = "Not persisted count is: " + countNotPersisted;
    }

    public void MapStateToProps(State state)
    {
        count = state.Get<StateCount>(StateCount.NAME).count;
        countNotPersisted = state.Get<StateCount>(StateCount.NAME).countNotPresisted;
    }
}
