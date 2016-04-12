using UnityEngine;
using System.Collections;

public class PinceTest : MonoBehaviour {
	public Animator anim;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{

		if (Input.GetMouseButton(0))
		{
			anim.SetTrigger(Animator.StringToHash("attack"));
		}
		else if (Input.GetMouseButton(1))
		{
			anim.SetTrigger(Animator.StringToHash("pain"));
		}
		else if (Input.GetMouseButton(2))
		{
			anim.SetTrigger(Animator.StringToHash("walk"));
		}
	}
}
