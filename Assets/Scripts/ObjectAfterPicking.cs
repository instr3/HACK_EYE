using UnityEngine;
using System.Collections;

public class ObjectAfterPicking : MonoBehaviour {
    public bool correct = false;//Default Value
    public Material CorrectMat, FalseMat;
    Material OriginMat;
    Renderer render;
    float allCounter;
    float counter;
    Color initColor;
	// Use this for initialization
	void Start () {
        render = GetComponent<Renderer>();
        OriginMat = render.material;

    }
    public bool OnPick(){
        render.material = correct ? CorrectMat : FalseMat;
        allCounter = correct ? 10 : 2;
        initColor = render.material.GetColor("_Color");
        counter = allCounter;
        if (correct)
            LevelManager.Instance.LevelFinished(transform.position);
        else
            LevelManager.Instance.WrongClickPerformed(transform.position);
        return correct;
        
    }
	// Update is called once per frame
	void Update () {
	    if(counter>0)
        {
            counter -= Time.fixedDeltaTime;
            if (counter <= 0)
            {
                render.material = OriginMat;
                return;
            }
            render.material.SetColor("_Color", Color.Lerp(initColor, Color.black, (allCounter-counter) / allCounter));

        }
    }
}
