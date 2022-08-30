
using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioManagerTutorial : MonoBehaviour
{
   
    private int firstPlayInt;
    public Slider musicSlider, soundEffectsSlider;
    private float musicFloat, soundEffectsFloat;
   // public AudioSource musicAudio;
   // public AudioSource[] soundEffectsAudio;
    public Slider ambientSlider;
    public AudioMixer ambientMixer, musicMixer, sfxMixer;


    void Start()
    {

        firstPlayInt = PlayerPrefs.GetInt("FirstPlay");

        if (firstPlayInt == 0)
        {
            //Debug.Log("First Start. Preferences set");
            musicFloat = .8f;
            soundEffectsFloat = .8f;
            musicSlider.value = musicFloat;
            soundEffectsSlider.value = soundEffectsFloat;
            ambientSlider.value = .8f;
            SaveSoundSettings();

            PlayerPrefs.SetInt("FirstPlay", -1);
        }
        else
        {
           // Debug.Log("Not First Start. Preferences loaded");
            LoadPreferences();
        }
    }

    public void LoadPreferences()
    {
        musicSlider.value = PlayerPrefs.GetFloat("MusicVol");
        soundEffectsSlider.value = PlayerPrefs.GetFloat("SFXVol");
        ambientSlider.value = PlayerPrefs.GetFloat("Ambient Volume");
     //   Debug.Log("Volume Sliders set");
    }


    public void BackButton()
    {
        SaveSoundSettings();
        SceneManager.LoadScene("MainMenu");
    }

    public void SaveSoundSettings()
    {
        if (ambientSlider.value == 0f)
        {
            PlayerPrefs.SetFloat("Ambient Volume", 0f);
            ambientMixer.SetFloat("Ambient", 0f);
        }
        else if (ambientSlider.value > 0f)
        {
            PlayerPrefs.SetFloat("Ambient Volume", ambientSlider.value);
            ambientMixer.SetFloat("Ambient", (ambientSlider.value * 50f) - 50f);
        }
        
        if (musicSlider.value == 0f)
        {
            PlayerPrefs.SetFloat("MusicVol", 0f);
            musicMixer.SetFloat("Music", 0f);
        }

        else if(musicSlider.value > 0f)
        {
            PlayerPrefs.SetFloat("MusicVol", musicSlider.value);
            musicMixer.SetFloat("Music", (musicSlider.value * 50f) - 50f);
        }

        if (soundEffectsSlider.value == 0f)
        {
            PlayerPrefs.SetFloat("SFXVol", 0f);
            sfxMixer.SetFloat("SFX", 0f);
        }

        else if (soundEffectsSlider.value > 0f)
        {

            PlayerPrefs.SetFloat("SFXVol", soundEffectsSlider.value);
            sfxMixer.SetFloat("SFX", (soundEffectsSlider.value * 50f) - 50f);
        }

    }

    void OnApplicationFocus(bool inFocus)
    {
        if(!inFocus)
        {
            SaveSoundSettings();
        }
    }

    public void UpdateSound()
    {
        SaveSoundSettings();
    }

}