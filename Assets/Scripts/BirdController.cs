using UnityEngine;
using System.Collections;

public class BirdController : MonoBehaviour {
    AudioSource audioSource;
    public Transform BirdCastPrefab;
	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
        Random.InitState(1415);
    }
	
	// Update is called once per frame
	void Update () {
	    if(!audioSource.isPlaying)
        {
            int value = Random.Range(0, 1000);
            Debug.Log(value);
            if(value >= 995)
            {
                audioSource.Play();
                Instantiate(BirdCastPrefab, transform.position, Quaternion.identity);
            }
        }
	}
}
