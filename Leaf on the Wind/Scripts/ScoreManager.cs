using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum ScrollerStates
{ 
    cutscene,
    running,
}

public enum DifficultyStates
{
    level1,
    level2,
    level3,
}

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI health;
    public TextMeshProUGUI score;
    public Text highScore;
    public TextMeshProUGUI gameOverText;

    public Player player;
    public Spawner spawner;

    [SerializeField] private float scrollSpeedHelper = -2f;
    public static float scrollSpeed = -2f;
    public ScrollerStates currentScrollerState;
    public DifficultyStates currentDifficulty;

    #region Gameplay Variables
    public float scoreCount;
    public float hiScoreCount;

    public float scrollSpeedLimiter;
    public float pointsPerSecond;
    public float speedIncreasePerSecond;

    public bool scoreIncreasing;

    [Space(5)]
    [Header("Lerp Variables")]
    public float lerpTime;

    #endregion
    /// <summary>
    /// Level 1: -20
    /// Level 2: -100
    /// Level 3: -300
    /// </summary>


    // Update is called once per frame
    void Update()
    {
        //Matches player state to scroller state
        switch (player.currentPlayerState) {
            case PlayerStates.cutscene:
                break;

            case PlayerStates.running:
                currentScrollerState = ScrollerStates.running;
                spawner.UpdateMasterSpawnerState(MasterSpawnerStates.enabled);
                break;

            case PlayerStates.crying:
                FallToStop();
                spawner.UpdateMasterSpawnerState(MasterSpawnerStates.disabled);
                break;
        }

        //Runner State Machine
        switch (currentScrollerState) {
            case ScrollerStates.cutscene:
                break;

            case ScrollerStates.running:
                AddtoScore();
                CurrentScoreTest();
                break;
        }

        switch (currentDifficulty)
        {
            case DifficultyStates.level1:
                CurrentScoreTest();
                SetDifficulty(-5);
                spawner.UpdateDifficulty(3);
                break;

            case DifficultyStates.level2:
                CurrentScoreTest();
                SetDifficulty(-40);
                spawner.UpdateDifficulty(2);
                break;

            case DifficultyStates.level3:
                CurrentScoreTest();
                SetDifficulty(-80);
                spawner.UpdateDifficulty(1.5f);
                break;
        }

        scrollSpeedHelper = scrollSpeed;
    }

    public void CurrentScoreTest()
    {
        if (scoreCount < 50)
            currentDifficulty = DifficultyStates.level1;
        if (scoreCount > 50 && scoreCount < 100)
            currentDifficulty = DifficultyStates.level2;
        if (scoreCount > 100 && scoreCount < 200)
            currentDifficulty = DifficultyStates.level3;
    }

    public void Reset()
    {
        scoreCount = 0;
        scrollSpeed = -2f;
        spawner.startTimeBtwSpawn = 5;
    }

    public void UpdateHealth(int healthNum)
    {
        health.text = "Health: " + healthNum.ToString();
    }

    void SetDifficulty(float speedLimiter)
    {
        ClampDifficulty(speedLimiter);
        scrollSpeed -= speedIncreasePerSecond * Time.deltaTime;
        pointsPerSecond = Mathf.Abs(scrollSpeed) / 2;
    }

    void ClampDifficulty(float clampLimit)
    {
        scrollSpeed = Mathf.Clamp(scrollSpeed, clampLimit, 0);
    }

    void AddtoScore()
    {
        scoreCount += pointsPerSecond * Time.deltaTime;
        score.text = "Score: " + Mathf.Round(scoreCount);
        //highScore.text = "High Score: " + hiScoreCount;
    }

    void FallToStop()
    {
        scrollSpeed = Mathf.Lerp(scrollSpeed, 0.9f, lerpTime * Time.deltaTime);
        scoreCount += pointsPerSecond * Time.deltaTime * Mathf.Abs(scrollSpeed);
        gameOverText.text = "Score: " + Mathf.Round(scoreCount);
        Debug.Log(scrollSpeed);
    }

}
