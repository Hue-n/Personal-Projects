using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void GetBackUp()
    {
        GetComponent<UIManager>().UpdateGameState(GameStates.get_back_up);
    }

    public void YouAreRight()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
