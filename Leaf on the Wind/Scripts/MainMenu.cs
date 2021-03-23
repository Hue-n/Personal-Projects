﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        GetComponent<UIManager>().UpdateGameState(GameStates.playing);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
