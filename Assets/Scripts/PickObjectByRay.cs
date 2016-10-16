using UnityEngine;
using System.Collections;

public class PickObjectByRay : MonoBehaviour {
    Ray ray;
    public Transform errorCastPrefab;
    LineRenderer linerenderer;
    // Use this for initialization
    void Start ()
    {
        linerenderer = GetComponent<LineRenderer>();
        if (linerenderer != null)
        {
            linerenderer.enabled = false;
        }
    }
    Ray CastRay()
    {
        return LevelManager.VR ? new Ray(transform.position, transform.forward) :
            Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
    }
    void GenerateErrorCasts(Vector3 position)
    {
        Transform tf = (Instantiate(errorCastPrefab, position,Quaternion.identity) as Transform);
    }
    // Update is called once per frame
    void Update ()
    {
        if (linerenderer != null && linerenderer.enabled)
        {
            ray = CastRay();
            RaycastHit hit;
            Vector3 targetPosition;
            if (Physics.Raycast(ray, out hit, float.PositiveInfinity))
            {
                targetPosition = hit.point;
            }
            else
            {
                targetPosition = ray.origin + ray.direction * 5000f;
            }
            linerenderer.SetPosition(0, ray.origin);
            linerenderer.SetPosition(1, targetPosition);
        }
        if (linerenderer != null && LevelManager.VRInputTrigger(SteamVR_Controller.ButtonMask.Trigger))
        {
            linerenderer.enabled = true;
        }
        if (Input.GetMouseButtonDown(0) || LevelManager.VRInputUpTrigger(SteamVR_Controller.ButtonMask.Trigger))
        {
            Debug.Log("Fire");
            if (linerenderer != null)linerenderer.enabled = false;
            ray = CastRay();
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, float.PositiveInfinity))
            {
                ObjectAfterPicking obj = hit.collider.GetComponent<ObjectAfterPicking>();
                if (obj!=null)
                {
                    obj.OnPick();
                }
                else
                {
                    GenerateErrorCasts(hit.point - ray.direction);
                }
            }

        }
    }
}
