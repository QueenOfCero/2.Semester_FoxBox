using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject levelSelection;


    public void GoToSampleScene()
    {
        SceneManager.LoadScene("BaseLevel");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void LevelSelect()
    {
        levelSelection.SetActive(true);
    }

    public void LevelSelectBack()
    {
        levelSelection.SetActive(false);
    }

    public void Fullscreen()
    {
        if (PlayerPrefs.GetInt("Fullscreen") == 1)
        {
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;

        }
        else
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;

        }
    }

    private void Start()
    {
        Fullscreen();
    }
}
