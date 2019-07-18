﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{

    void Start()
    {
       // Screen.SetResolution(750, 900, false);
    }

    public void PlayClicked()
    {
        SceneManager.LoadScene("Cutscene_Opening");
        //ControllerScript.setPauseState(false);
    }
    public void HelpClicked()
    {
       // SceneManager.LoadScene("Menu_LevelSelect");
    }
    public void LevelSelectClicked()
    {
        SceneManager.LoadScene("Menu_LevelSelect");
    }
    public void QuitClicked()
    {
        Application.Quit();
    }
}
