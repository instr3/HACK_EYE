using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    int MaxLife = 10;
    int Life;
    public bool VREnabled;
    public int LoadLevelID;
    public Transform[] LevelPrefabs;
    public Transform ElevatorCastPrefab;
    int elevatorHeight = 200;
    float elevatorTime = 20f;
    float triggerPeriod = 0.4f;
    public bool ElevatorAscending;
    Transform currentLevel;
    Transform nextLevel;
    public static LevelManager Instance;
    public AudioSource ElevatorAudioSource;
    public AudioClip ErrorSound, CorrectSound;
    float startTime;
    float lastTriggerTime;
    public bool GameOver;
    public static bool VR
    {
        get { return Instance.VREnabled; }
    }
    public SteamVR_TrackedObject VRTrackedObject;
    public static SteamVR_Controller.Device GetVRDevice()
    {
        try
        {
            return SteamVR_Controller.Input((int)(Instance.VRTrackedObject.index));
        }
        catch
        {
            return null;
        }
    }
    public static bool VRInputTrigger(ulong triggerMask)
    {
        if (!VR) return false;
        SteamVR_Controller.Device device = GetVRDevice();
        if (device == null) return false;
        return device.GetPressDown(triggerMask);
    }
    public static bool VRInputUpTrigger(ulong triggerMask)
    {
        if (!VR) return false;
        SteamVR_Controller.Device device = GetVRDevice();
        if (device == null) return false;
        return device.GetPressUp(triggerMask);
    }
    // Use this for initialization
    void Start () {
        Instance = this;
        currentLevel = Instantiate(LevelPrefabs[LoadLevelID]);
        StartNewLevel(LoadLevelID);
    }
	public void LevelFinished(Vector3 position)
    {
        if (GameOver) return;
        AudioSource.PlayClipAtPoint(CorrectSound, position);
        if (ElevatorAscending)
        {
            return;
        }
        if(LoadLevelID<LevelPrefabs.Length-1)
        {
            LoadLevelID++;
            nextLevel = Instantiate(LevelPrefabs[LoadLevelID]);
            nextLevel.position += Vector3.up * elevatorHeight;
            ElevatorAscending = true;
            startTime = Time.time + 2f;
            ElevatorAudioSource.Play();
        }
    }
    public void StartNewLevel(int id)
    {
        Life = MaxLife;
        UI_LevelHint.Instance.OnLevelStart(id);
    }
    public void WrongClickPerformed(Vector3 position)
    {
        if (ElevatorAscending) return;
        AudioSource.PlayClipAtPoint(ErrorSound, position);
        if (Life == 0)
        {
            UI_TextbarControl.Instance.ShowText("Press [t|s] to restart.", 0f, 1.5f, Color.red);
            UI_TextbarControl.Instance.Lock();
            GameOver = true;
            return;
        }
        else UI_TextbarControl.Instance.ShowText("-" + Life.ToString(), 0f, 1.5f, Color.red);
        --Life;

    }
	// Update is called once per frame
	void Update () {
	    if(ElevatorAscending)
        {
            if(Time.time-lastTriggerTime>=triggerPeriod)
            {
                lastTriggerTime = Time.time;
                Instantiate(ElevatorCastPrefab, Vector3.zero, Quaternion.identity);
            }
            float value = (Time.time - startTime) / elevatorTime;
            float newY = Mathf.Lerp(elevatorHeight, 0, value);
            nextLevel.position = new Vector3(nextLevel.position.x, newY, nextLevel.position.z);
            currentLevel.position = new Vector3(currentLevel.position.x, newY - elevatorHeight, currentLevel.position.z);
            if (value>1)
            {
                ElevatorAscending = false;
                Destroy(currentLevel.gameObject);
                currentLevel = nextLevel;
                StartNewLevel(LoadLevelID);
                ElevatorAudioSource.Stop();
            }
        }
	}
}
