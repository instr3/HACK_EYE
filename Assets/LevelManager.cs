using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {
    public int LoadLevelID;
    public Transform[] LevelPrefabs;
    public Transform ElevatorCastPrefab;
    int elevatorHeight = 200;
    float elevatorTime = 20f;
    float triggerPeriod = 0.4f;
    bool elevatorAscending;
    Transform currentLevel;
    Transform nextLevel;
    public static LevelManager Instance;
    float startTime;
    float lastTriggerTime;
    // Use this for initialization
    void Start () {
        Instance = this;
        currentLevel = Instantiate(LevelPrefabs[LoadLevelID]);
    }
	public void LevelFinished()
    {
        if(elevatorAscending)
        {
            return;
        }
        if(LoadLevelID<LevelPrefabs.Length-1)
        {
            LoadLevelID++;
            nextLevel = Instantiate(LevelPrefabs[LoadLevelID]);
            nextLevel.position += Vector3.up * elevatorHeight;
            elevatorAscending = true;
            startTime = Time.time + 2f;
        }
    }
	// Update is called once per frame
	void Update () {
	    if(elevatorAscending)
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
                elevatorAscending = false;
                Destroy(currentLevel.gameObject);
                currentLevel = nextLevel;
            }
        }
	}
}
