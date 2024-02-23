using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    float stopwatch = 0.0f;

    public static LevelManager instance;

    [SerializeField] GameObject winScreen;
    [SerializeField] GameObject loseScreen;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        stopwatch += Time.deltaTime;
    }

    public void OnGoalReached()
    {
        Time.timeScale = 0;
        print(string.Format("Goal reached in {0} seconds", stopwatch));
        winScreen.SetActive(true);
    }

    public void OnPlayerDeath()
    {
        loseScreen.SetActive(true);
    }
}
