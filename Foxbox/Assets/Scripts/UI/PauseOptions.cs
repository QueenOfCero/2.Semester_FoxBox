using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseOptions : MainCollectibles
{
    public Canvas canvas;
    public Camera cam;

    public Slider sfx;
    public Slider music;
    public Slider ambient;
    public AudioMixer ambientMixer, musicMixer, sfxMixer;

    // public GameObject cameraController;
    // Start is called before the first frame update
    void Start()
    {
       
        canvas.worldCamera = cam;
        SetSliders();
    }

    private void SetSliders()
    {
        sfx.value = PlayerPrefs.GetFloat("SFXVol");
        music.value = PlayerPrefs.GetFloat("MusicVol");
        ambient.value = PlayerPrefs.GetFloat("Ambient Volume");

    }



    public void Fullscreen(bool is_fullscene)
    {
        if (is_fullscene)
        {
            PlayerPrefs.SetInt("Fullscreen", 1);
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
        }
        else
        {
            PlayerPrefs.SetInt("Fullscreen", 0);
            Screen.fullScreenMode = FullScreenMode.Windowed;
        }



    }
}
