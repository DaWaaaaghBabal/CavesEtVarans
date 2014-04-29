using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public float moveSensitivity;
    public float zoomSensitivity;
    public int detectionRange;
	
	void Update () {
        Vector2 mousePos = Input.mousePosition;
        float leftRight = 0, topBottom = 0, forwardBackward = Input.GetAxis("Mouse ScrollWheel");

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
        MoveCamera(leftRight, topBottom);
        ZoomCamera(forwardBackward);
	}

    private void ZoomCamera(float forwardBackward) {
        transform.position += Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0) * (zoomSensitivity * new Vector3(0, 0, forwardBackward));
    }

    private void MoveCamera(float leftRight, float topBottom) {
        Vector3 delta = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0) * new Vector3(leftRight, 0, topBottom);
        delta.Normalize();
        transform.position += moveSensitivity * Time.deltaTime * delta;
    }
}
