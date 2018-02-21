using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStories;

public class Button : MonoBehaviour
{
    public Store store;

    public void OnClick_Inc()
    {
        store.Dispatch(new ActionIncrementCount());
    }

    public void OnClick_Dec()
    {
        store.Dispatch(new ActionDecrementCount());
    }
}
