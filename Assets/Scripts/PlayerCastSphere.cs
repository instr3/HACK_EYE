using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PlayerCastSphere : MonoBehaviour {
    public Transform castPrefab;
    public AudioSource source;
    List<Transform> castObjects;

	// Use this for initialization
	void Start () {
        castObjects = new List<Transform>();

    }
    IEnumerator GenerateCasts()
    {
        Vector3 pos = transform.position;
        for(int i=1;i<=5;++i)
        {
            Transform tf = (Instantiate(castPrefab, pos, Quaternion.identity) as Transform);
            //tf.GetComponent<SphereBehaviour>().
            yield return new WaitForSeconds(0.2f);
        }
    }
    IEnumerator StartHapticVibrationCoroutine(SteamVR_Controller.Device device, float length, float strength) {

        for(float i = 0; i < length; i += Time.deltaTime) {
            device.TriggerHapticPulse((ushort)Mathf.Lerp(0, 3999, strength));
            yield return null;
        }
    }
    // Update is called once per frame
    void Update () {
        if(Input.GetKeyDown(KeyCode.Space)||LevelManager.VRInputTrigger(SteamVR_Controller.ButtonMask.Touchpad))
        {
            if(LevelManager.Instance.GameOver)
            {
                SceneManager.LoadScene(0);
            }
            Debug.Log("yell");
            source.Play();
            StartCoroutine(GenerateCasts());
            if (LevelManager.VR)
                StartCoroutine(StartHapticVibrationCoroutine(LevelManager.GetVRDevice(), 1, 1));
            //Transform tf = (Instantiate(castPrefab, transform.position,Quaternion.identity) as Transform);
            //castObjects.Add(tf);
        }
	}
}
