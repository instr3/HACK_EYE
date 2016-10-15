using UnityEngine;
using System.Collections;

public class PickObjectByRay : MonoBehaviour {
    Ray ray;
    public Transform errorCastPrefab;
    // Use this for initialization
    void Start ()
    {

    }

    void GenerateErrorCasts(Vector3 position)
    {
        Transform tf = (Instantiate(errorCastPrefab, position,Quaternion.identity) as Transform);
    }
    // Update is called once per frame
    void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Fire");
            ray = Camera.main.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
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
