using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    float[] recordTimes = null;

    int lastPlayedLevel = 0;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            // Since floats are non-nullable, we use an obviously invalid value
            // (e.g., negative "record time") to signify that there is no relevant data yet.
            recordTimes = new float[2] {-1, -1};
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetRecordTime(int level, float time)
    {
        recordTimes[level - 1] = time;
    }

    public float GetRecordTime(int level)
    {
        return recordTimes[level - 1];
    }

    public void SetLastPlayedLevel(int level)
    {
        lastPlayedLevel = level;
    }

    public int GetLastPlayedLevel()
    {
        return lastPlayedLevel;
    }
}
