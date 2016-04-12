using UnityEngine;
using CavesEtVarans.utils;
using UnityEngine.UI;

public class GaugeDisplay : MonoBehaviour, Observer<GaugeChange> {
	void Start() {
		GetComponent<Renderer>().material.color = new Color(Random.Range(0, 1), Random.Range(0, 1), Random.Range(0, 1));
	}
	
	void Update() {
		GetComponent<Renderer>().material.SetFloat("_Cutoff", 0.01f + 1.1f * Mathf.InverseLerp(20, Screen.width, Input.mousePosition.x));
    }
	void Observer<GaugeChange>.Update(GaugeChange data) {
		GetComponent<Renderer>().material.SetFloat("_Cutoff", 1.01f - (float) data.Percentage);
	}
}
