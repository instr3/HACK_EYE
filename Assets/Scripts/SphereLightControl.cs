using UnityEngine;
using System.Collections;

public class SphereLightControl : MonoBehaviour {
    SphereBehaviour sphereBehaviour;
    Light light;
    // Use this for initialization
    void Start () {
        sphereBehaviour = GetComponent<SphereBehaviour>();
        light = GetComponent<Light>();
    }
	
	// Update is called once per frame
	void Update () {
        light.color = sphereBehaviour.currentColor;
    }
}
