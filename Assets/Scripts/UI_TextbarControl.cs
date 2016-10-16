using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_TextbarControl : MonoBehaviour
{
    public static UI_TextbarControl Instance;
    public Text Text1, Text2;
    float startFadingTime;
    float fadeTime;
    bool shown;
    bool locked;
    float startTime;
    void UpdateContentAlpha(float a)
    {
        Text1.color = new Color(Text1.color.r, Text1.color.g, Text1.color.b, a);
        Text2.color = new Color(Text2.color.r, Text2.color.g, Text2.color.b, a);
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
    public void Lock()
    {
        locked = true;
    }
    public void ShowText(string text, float inputStartFadingTime, float inputFadingTime, Color textColor)
    {
        if (locked) return;
        text = text.Replace("[t|s]", LevelManager.VR ? "touchpad" : "spacebar");
        text = text.Replace("[T|S]", LevelManager.VR ? "Touchpad" : "Spacebar");
        text = text.Replace("[t|l]", LevelManager.VR ? "trigger" : "leftmouse");
        text = text.Replace("[T|L]", LevelManager.VR ? "Trigger" : "Leftmouse");
        startTime = Time.time + startFadingTime;
        Text1.text = text;
        Text2.text = text;
        startFadingTime = inputStartFadingTime;
        fadeTime = inputFadingTime;
        Text1.color = textColor;
        Text2.color = textColor;
        UpdateContentAlpha(0);
        shown = true;
    }

    void Update()
    {
        if (shown)
        {
            float ap = 1 - (Time.time - startTime) / fadeTime;
            if (ap > 1) ap = 1;
            if (ap <= 0)
            {
                UpdateContentAlpha(0);
                shown = false;
            }
            UpdateContentAlpha(ap);
        }
    }
}
