using UnityEngine;
using System.Collections;

public class TileGenerator : MonoBehaviour {
	
	public GameObject tile;
	// Use this for initialization
	void Start () {
		for(int i = 0; i<10; i++) {
			for(int j = 0; j<10; j++) {
				Vector3 position = new Vector3(i, 0, j);
				Instantiate(tile, position, transform.rotation);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
