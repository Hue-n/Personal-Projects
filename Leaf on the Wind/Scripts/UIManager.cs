using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

    public enum GameStates
    { 
        main_menu,
        paused,
        playing,
        game_over,
        get_back_up
    }

public class UIManager : MonoBehaviour
{
    public GameStates currentGameState;

    [Header("Gameplay References")]
    public ScoreManager scoreManager;
    public LeafSpawnSystem leafSpawnSystem;
    public Player player;

    [Header("UI Panel References")]

    public GameObject playingStateUI;
    public GameObject pauseMenuStateUI;
    public GameObject gameOverStateUI;
    public GameObject mainMenuStateUI;

    private void Awake()
    {
        currentGameState = GameStates.main_menu;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentGameState) {
            case GameStates.main_menu:
                MainMenu();
                break;
            case GameStates.paused:
                Pause();
                break;
            case GameStates.playing:
                leafSpawnSystem.UpdateLeafSpawnState(LeafSystemStates.enabled);
                Playing();
                break;
            case GameStates.game_over:
                leafSpawnSystem.UpdateLeafSpawnState(LeafSystemStates.disabled);
                GameOver();
                break;
            case GameStates.get_back_up:
                GetBackUp();
                UpdateGameState(GameStates.playing);
                break;
        }
    }

    public void UpdateGameState(GameStates newGameState)
    {
        currentGameState = newGameState;
    }

    public void MainMenu()
    {
        scoreManager.Reset();
        Time.timeScale = 0;
        pauseMenuStateUI.SetActive(false);
        mainMenuStateUI.SetActive(true);
        playingStateUI.SetActive(false);
        gameOverStateUI.SetActive(false);
    }

    public void Playing()
    {
        Time.timeScale = 1;
        player.UpdatePlayerState(PlayerStates.running);
        pauseMenuStateUI.SetActive(false);
        mainMenuStateUI.SetActive(false);
        playingStateUI.SetActive(true);
        gameOverStateUI.SetActive(false);
    }

    public void Pause()
    {
        Time.timeScale = 0;
        pauseMenuStateUI.SetActive(true);
        mainMenuStateUI.SetActive(false);
        playingStateUI.SetActive(false);
        gameOverStateUI.SetActive(false);
    }

    public void GameOver()
    {
        Time.timeScale = 1;
        pauseMenuStateUI.SetActive(false);
        mainMenuStateUI.SetActive(false);
        playingStateUI.SetActive(false);
        gameOverStateUI.SetActive(true);
    }

    public void LerpStopScroll()
    { 
    
    }

    public void GetBackUp()
    {
        scoreManager.Reset();
        player.currentHealth = GameManager.maxPlayerHealth;
    }
}
