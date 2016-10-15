using UnityEngine;
using System.Collections;
using VRTK;

public class PickObjectByRayVR : MonoBehaviour
{
    Ray ray;
    public Transform errorCastPrefab;
    public VRTK_ControllerEvents controller;
    // Use this for initialization
    void Start()
    {
        if (controller == null)
        {
            controller = GetComponent<VRTK_ControllerEvents>();
        }
        controller.AliasGrabOn += new ControllerInteractionEventHandler(OnFire);
    }
    void GenerateErrorCasts(Vector3 position)
    {
        Transform tf = (Instantiate(errorCastPrefab, position, Quaternion.identity) as Transform);
    }
    void OnFire(object objref,ControllerInteractionEventArgs args)
    {
        Debug.Log("Fire");
        ray = Camera.main.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, float.PositiveInfinity))
        {
            ObjectAfterPicking obj = hit.collider.GetComponent<ObjectAfterPicking>();
            if (obj != null)
            {
                obj.OnPick();
            }
            else
            {
                GenerateErrorCasts(hit.point - ray.direction);
            }
        }


    }
    // Update is called once per frame
    void Update()
    {
    }
}
