using UnityEngine;
using System.Collections;

public class SphereBehaviour : MonoBehaviour
{
    public Color MainColor = Color.black;
    public Color HighColor = Color.green;
    public float thershold = 1f;
    public float FadingOut = 0f;// 0: never fade out
    public float MaxRadius = 1000f; //Default Value
    public float ExpandSpeed;// Default Value
    [HideInInspector]
    public Color currentColor;
    float radius;
    Material mat;
    float startTime;
    // Use this for initialization
	void Start () {
        startTime = Time.time;
        radius = 0;
        mat = GetComponent<Renderer>().material;
        mat.SetColor("_RegularColor", MainColor);
        mat.SetColor("_HighlightColor", HighColor);
        mat.SetFloat("_HighlightThresholdMax", thershold);
        currentColor = HighColor;

    }
	
	// Update is called once per frame
	void Update () {
        radius = radius + ExpandSpeed * Time.deltaTime;
        if (radius > MaxRadius)
        {
            Destroy(gameObject);
        }
        transform.localScale = new Vector3(radius, radius, radius);
        if(FadingOut>0)
        {
            float value = (Time.time - startTime) / FadingOut;
            Color currentColor = Color.Lerp(HighColor, MainColor, value);
            if(value>=1)
            {
                Destroy(gameObject);
            }
            mat.SetColor("_HighlightColor", currentColor);
        }
    }
}
