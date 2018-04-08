using UnityEngine;

public class FollowTarget_Example2 : MonoBehaviour 
{
	private Vector2 target;
	
	void Update () 
	{
		transform.position = Vector3.Lerp(transform.position, target, 6f * Time.deltaTime);
	}

	public void SetTarget(Vector2 mousePos)
	{
		target = Camera.main.ScreenToWorldPoint(mousePos);
	}
}
