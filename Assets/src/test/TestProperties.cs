using UnityEngine;
using System.Collections;
using CavesEtVarans.utils;

public class TestProperties : MonoBehaviour {
	public GUIText text;
	public bool needsTesting;
	// Use this for initialization
	void Start () {
		if (needsTesting) {
			Properties props = new Properties ("testProperties");
			string str = props.GetInt ("int") + " " + props.GetStr ("string");
			text.text = str;
		}
	}
}
