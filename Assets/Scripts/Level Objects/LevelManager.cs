using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using NUnit.Framework.Constraints;

public class LevelManager : MonoBehaviour
{
    [SerializeField] float timeLimit;
    float stopwatch = 0.0f;

    bool gameOver = false;

    public static LevelManager instance;

    PlayerMovement playerMover;

    [SerializeField] ParticleSystem goalParticles;
    [SerializeField] Image endScreenFade;
    [SerializeField] GameObject winScreen;
    [SerializeField] TextMeshProUGUI countdownTimer;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerMover = FindFirstObjectByType<PlayerMovement>();

        goalParticles.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        stopwatch += Time.deltaTime;
        if (!gameOver && stopwatch > timeLimit)
        {
            EndLevel();
            StartCoroutine(LoseSequence());
        }

        int secondsRemaining = Mathf.FloorToInt(timeLimit - stopwatch);
        int centisecondsRemaining = (int)(100 * (timeLimit - stopwatch)) % 100;
        string timeString = string.Format("Time Left: {0:00}'{1:00}\"", secondsRemaining, centisecondsRemaining);
        countdownTimer.text = timeString;
    }

    public void OnGoalReached()
    {
        EndLevel();
        goalParticles.Play();
        StartCoroutine(WinSequence());
    }

    public void OnPlayerDeath()
    {
        EndLevel();
        StartCoroutine(LoseSequence());
    }

    void EndLevel()
    {
        gameOver = true;
        countdownTimer.enabled = false;
        playerMover.Teardown();
    }

    IEnumerator WinSequence()
    {
        endScreenFade.enabled = true;
        for (int step = 0; step <= 60; step++)
        {
            endScreenFade.color = new Color(1, 1, 1, (float)step / 60f);
            Camera.main.orthographicSize = 5 - 4 * (float)step / 60f;
            yield return new WaitForSeconds(0.05f);
        }
        endScreenFade.enabled = false;
        winScreen.SetActive(true);
    }

    IEnumerator LoseSequence()
    {
        endScreenFade.enabled = true;
        for (int step = 0; step <= 20; step++)
        {
            endScreenFade.color = new Color(0, 0, 0, (float)step / 20f);
            yield return new WaitForSeconds(0.1f);
        }
        SceneManager.LoadScene("gameOver");
    }
}
