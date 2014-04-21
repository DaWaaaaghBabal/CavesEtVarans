using UnityEngine;
using System.Collections;

public class MoveToTile : MonoBehaviour {

	public Color mouseOverColor;
	public Color idleColor;
	
	// Use this for initialization
	void OnMouseDown(){
		CharacterMovement movement = CharacterManager.GetActiveCharacter().GetComponent<CharacterMovement>();
		movement.SetTarget(gameObject);
	}
	
	void OnMouseOver() {
		renderer.material.color = mouseOverColor;
	}
	
	void OnMouseExit(){
		renderer.material.color = idleColor;
	}
	
	void Start() {
		renderer.material.color = idleColor;
	}
}
