using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BreakMenu : AudioManagerTutorial
{
    public GameObject menu;
    public GameObject optionsMenu;
    public GameObject controlsMenu;
    bool breaker = false;

 
    void Update()
    {
       if (Input.GetButtonUp("Pause") || Input.GetButtonUp("PAUSE"))
       {
            Break();
       }
    }

    private void Break()
    {
        if (breaker == false && menu != null)
        {
            menu.SetActive(true);
            Time.timeScale = 0f;
            breaker = true;
        }
        else if (menu != null)
        {
            menu.SetActive(false);
            Time.timeScale = 1f;
            breaker = false;
        }
    }

    public void GoToMainMenu()
    {
        PlayerPrefs.Save();
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void GoToOptions()
    {
        // Time.timeScale = 1f;
        // SceneManager.LoadScene("OptionsMenu");

        optionsMenu.SetActive(true);
        LoadPreferences();
        menu.SetActive(false);

    }

    public void GoBackToPause()
    {
        // Time.timeScale = 1f;
        // SceneManager.LoadScene("OptionsMenu");
        PlayerPrefs.Save();
        menu.SetActive(true);
        optionsMenu.SetActive(false);
    }

    public void GoToSensitivity()
    {
        Time.timeScale = 1f;
        controlsMenu.SetActive(true);
    }

    public void GoBackToOptions()
    {
        PlayerPrefs.Save();
        controlsMenu.SetActive(false);
        Time.timeScale = 0f;
    }

    public void Play()
    {
        PlayerPrefs.Save();
        menu.SetActive(false);
        Time.timeScale = 1f; 
        breaker = false;
        menu.SetActive(false);
    }

    public void Restart() 
        {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

}
