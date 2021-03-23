using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool isPaused = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isPaused == false)
        {
            GetComponent<UIManager>().UpdateGameState(GameStates.paused);
        }

        if (Input.GetKeyDown(KeyCode.Escape) && isPaused == true)
        {
            GetComponent<UIManager>().UpdateGameState(GameStates.playing);
        }
    }

    public void mainMenu()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void resume()
    {
        GetComponent<UIManager>().UpdateGameState(GameStates.playing);
    }
}
