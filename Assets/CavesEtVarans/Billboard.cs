using UnityEngine;
using System.Collections;

public class Billboard : MonoBehaviour {

	// Update is called once per frame
	void Update () {
        Transform cameraTransform = Camera.allCameras[0].transform;
        transform.LookAt(cameraTransform);
        transform.Rotate(Vector3.right * -90);
	}
}
