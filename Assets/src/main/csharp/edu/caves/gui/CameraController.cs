using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public float cameraSensitivity;
    public int detectionRange;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 mousePos = Input.mousePosition;
        int leftRight = 0, topBottom = 0;
        if (mousePos.x <= detectionRange || Input.GetKeyDown(KeyCode.LeftArrow)) {
            leftRight = -1;
        } else if (mousePos.x >= Screen.width - detectionRange || Input.GetKeyDown(KeyCode.RightArrow)) {
            leftRight = 1;
        }

        if (mousePos.y <= detectionRange || Input.GetKeyDown(KeyCode.DownArrow)) {
            topBottom = -1;
        } else if (mousePos.y >= 0.9 * Screen.height - detectionRange || Input.GetKeyDown(KeyCode.UpArrow)) {
            topBottom = 1;
        }

        Vector3 delta = Quaternion.Euler(0, 45, 0) * new Vector3(leftRight, 0, topBottom);
        delta.Normalize();
        transform.position += cameraSensitivity * delta;
	}
}
