using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour {
	private Vector3 start;
	private Vector3 target;
	private float distance = 0;
	private float startTime;
	public int speed = 1;
	
	// Use this for initialization
	void Start () {
		CharacterManager.AddCharacter(gameObject);
		start = transform.position;
		target = start;
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position != target) {
			float distCovered = (Time.time - startTime) * speed;
        	float fracDist = distCovered / distance;
			Vector3 newPosition = Vector3.Lerp(start, target, fracDist);
			transform.position = newPosition;
		}
	}
	
	public void SetTarget(GameObject targetObject) {
		target = targetObject.transform.position;
		start = transform.position;
		Vector3 delta = target - start;
		distance = delta.magnitude;
		startTime = Time.time;
	}
}
