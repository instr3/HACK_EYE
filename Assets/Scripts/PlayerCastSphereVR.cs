using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using VRTK;


public class PlayerCastSphereVR : MonoBehaviour {
    public Transform castPrefab;
    List<Transform> castObjects;
    VRTK_ControllerEvents controller;

    // Use this for initialization
    void Start () {
        castObjects = new List<Transform>();
        if (controller == null)
        {
            controller = GetComponent<VRTK_ControllerEvents>();
        }
        controller.AliasMenuOn += new ControllerInteractionEventHandler(OnFire);

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
    void OnFire(object objref, ControllerInteractionEventArgs args)
    {
        StartCoroutine(GenerateCasts());
    }
    // Update is called once per frame
    void Update () {

	}
}
