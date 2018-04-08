using UnityEngine;
using UnityEngine.UI;

public class CountText_Example1 : MonoBehaviour 
{
    public Text countText;
    public Text countNotPersistedText;

    public void SetCountText(int count)
    {
        countText.text = "Count is: " + count;
    }

    public void SetCountTextNotPersisted(int count)
    {
        countNotPersistedText.text = "Not persisted count is: " + count;
    }
}
