using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_LevelHint : MonoBehaviour {
    public static UI_LevelHint Instance;
    public Text Text1, Text2;
    public Image HintImage;
    public Sprite[] HintList;
    float startFadingTime = 5f;
    float fadeTime = 5f;
    bool shown;
    float startTime;
    void UpdateContentAlpha(float a)
    {
        Text1.color = new Color(Text1.color.r, Text1.color.g, Text1.color.b, a);
        Text2.color = new Color(Text2.color.r, Text2.color.g, Text2.color.b, a);
        HintImage.color = new Color(HintImage.color.r, HintImage.color.g, HintImage.color.b, a);
    }
    void Awake()
    {
        Instance = this;
        shown = false;
    }
    void Start()
    {
        UpdateContentAlpha(0);
    }
    public void OnLevelStart(int levelID)
    {
        startTime = Time.time + startFadingTime;
        HintImage.sprite = HintList[levelID];
        shown = true;
    }
    void Update()
    {
        if(shown)
        {
            float ap = 1 - (Time.time - startTime) / fadeTime;
            if (ap >1) ap = 1;
            if(ap<=0)
            {
                UpdateContentAlpha(0);
                shown = false;
            }
            UpdateContentAlpha(ap);
        }
    }
}
