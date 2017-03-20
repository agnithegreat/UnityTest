using UnityEngine;

public class BGScroller : MonoBehaviour
{
	public float scrollSpeed;
	public float scrollLength;

	private Vector3 _startPosition;

	void Start ()
	{
	    _startPosition = transform.position;
	}

	void Update ()
	{
		float newPosition = Mathf.Repeat(Time.time * scrollSpeed, scrollLength);
		transform.position = _startPosition + Vector3.up * newPosition;
	}
}