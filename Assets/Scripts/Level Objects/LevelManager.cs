using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using NUnit.Framework.Constraints;

public class LevelManager : MonoBehaviour
{
    [SerializeField] int level = 1;
    [SerializeField] float timeLimit;
    float stopwatch = 0.0f;

    bool gameOver = false;
    float levelEndTime;
    bool newRecord = false;

    public static LevelManager instance;

    PlayerMovement playerMover;

    [SerializeField] ParticleSystem goalParticles;
    [SerializeField] Image endScreenFade;
    [SerializeField] GameObject winScreen;
    [SerializeField] GameObject nextLevelButton;
    [SerializeField] TextMeshProUGUI newRecordNotice;
    [SerializeField] TextMeshProUGUI countdownTimer;

    AudioSource audioSource;
    [SerializeField] AudioClip winSound;
    [SerializeField] AudioClip deathSound;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerMover = FindFirstObjectByType<PlayerMovement>();
        audioSource = GetComponent<AudioSource>();
        GameManager.Instance.SetLastPlayedLevel(level);
        goalParticles.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        stopwatch += Time.deltaTime;
        if (!gameOver && stopwatch > timeLimit)
        {
            EndLevel();

            audioSource.clip = deathSound;
            audioSource.Play();

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
        
        audioSource.clip = winSound;
        audioSource.Play();

        float recordTime = GameManager.Instance.GetRecordTime(level);
        if (recordTime <= 0 || GameManager.Instance.GetRecordTime(level) > levelEndTime)
        {
            GameManager.Instance.SetRecordTime(level, levelEndTime);
            newRecord = true;
        }

        StartCoroutine(WinSequence());
    }

    public void OnPlayerDeath()
    {
        EndLevel();

        audioSource.clip = deathSound;
        audioSource.Play();

        StartCoroutine(LoseSequence());
    }

    void EndLevel()
    {
        gameOver = true;
        levelEndTime = stopwatch;
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
        if (newRecord)
        {
            newRecordNotice.gameObject.SetActive(true);

            int seconds = Mathf.FloorToInt(levelEndTime);
            int centiseconds = (int)(100 * levelEndTime) % 100;
            string noticeText = string.Format("{0:00}'{1:00}\" - New record!", seconds, centiseconds);
            newRecordNotice.text = noticeText;
        }

        if (level < 2)
        {
            nextLevelButton.SetActive(true);
        }
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
