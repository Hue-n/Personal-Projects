using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateDisplayer : MonoBehaviour
{
    [Header("States")]
    public GameStates gameState;
    public PlayerStates playerState;
    public ScrollerStates scrollerState;
    public DifficultyStates difficultyState;

    [Space(2)]
    [Header("Game Variables")]
    public float score;
    public float scrollSpeed;

    [Space(2)]

    [Header("References")]
    public UIManager uiManager;
    public Player player;
    public ScoreManager scoreManager;
    
    // Update is called once per frame
    void Update()
    {
        playerState = player.currentPlayerState;
        gameState = uiManager.currentGameState;
        scrollerState = scoreManager.currentScrollerState;
        score = scoreManager.scoreCount;
        scrollSpeed = ScoreManager.scrollSpeed;
        difficultyState = scoreManager.currentDifficulty;
    }
}
