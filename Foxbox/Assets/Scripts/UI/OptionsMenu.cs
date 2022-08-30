using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{

    public Slider musicSlider, sfxSlider, ambientSlider;
    public AudioMixer ambientMixer, musicMixer, sfxMixer;

    public Toggle fullScreen;

    private int firstPlayInt;


    void Start()
    {
        Debug.Log("Options started");

        
        

        firstPlayInt = PlayerPrefs.GetInt("FirstPlay");

        if (firstPlayInt == 0)
        {
            Debug.Log("First Start. Preferences set");

            SaveSoundSettings();

            PlayerPrefs.SetInt("FirstPlay", -1);
        }
        else
        {
            Debug.Log("Not First Start. Preferences loaded");
            LoadPreferences();
        }
    }

    private void Update()
    {
        if (Screen.fullScreenMode == FullScreenMode.ExclusiveFullScreen)
        {
            fullScreen.isOn = true;
        }
        else
        {
            
            fullScreen.isOn = false;
        }
    }



    public void LoadPreferences()
    {
        //musicSlider.value = 0f;
        //sfxSlider.value = 0.5f;
        //ambientSlider.value = 1f;

        musicSlider.value = PlayerPrefs.GetFloat("MusicVol");
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVol");
        ambientSlider.value = PlayerPrefs.GetFloat("Ambient Volume");
        Debug.Log("Volume Sliders set");
    }


    public void BackButton()
    {
        SaveSoundSettings();
        SceneManager.LoadScene("MainMenu");
    }

    public void SaveSoundSettings()
    {
        PlayerPrefs.Save();
    }

    public void SaveMusic()
    {
        if (musicSlider.value == 0f)
        {
            PlayerPrefs.SetFloat("MusicVol", 0f);
            musicMixer.SetFloat("Music", 0f);
        }

        else
        {

            PlayerPrefs.SetFloat("MusicVol", musicSlider.value);
            musicMixer.SetFloat("Music", (musicSlider.value * 50f) - 50f);
        }
    }
    public void SaveSFX()
    {
        if (sfxSlider.value == 0f)
        {
            PlayerPrefs.SetFloat("SFXVol", 0f);
            sfxMixer.SetFloat("SFX", 0f);
        }

        else
        {
            PlayerPrefs.SetFloat("SFXVol", sfxSlider.value);
            sfxMixer.SetFloat("SFX", (sfxSlider.value * 50f) - 50f);

        }
    }

    public void SaveAmbient()
    {
        if (ambientSlider.value == 0f)
        {
            PlayerPrefs.SetFloat("Ambient Volume", 0f);
            ambientMixer.SetFloat("Ambient", 0f);
        }

            else {
                    PlayerPrefs.SetFloat("Ambient Volume", ambientSlider.value);
                    ambientMixer.SetFloat("Ambient", (ambientSlider.value * 50f) - 50f);
                }
    }

    public void Fullscreen(bool is_fullscene)
    {
        if(is_fullscene)
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
