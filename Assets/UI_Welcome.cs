using UnityEngine;
using System.Collections;

public class UI_Welcome : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || LevelManager.VRInputTrigger(SteamVR_Controller.ButtonMask.Touchpad))
        {
            Destroy(gameObject);
        }
    }
}
