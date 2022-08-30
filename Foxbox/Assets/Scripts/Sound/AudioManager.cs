using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    public string backgroundMusic;

    private float aVolume;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

      // aVolume = PlayerPrefs.GetFloat("Ambient Volume");
      //  Debug.Log(aVolume);

        DontDestroyOnLoad(gameObject);

        string name = SceneManager.GetActiveScene().name;


        foreach (var sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;


            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;

        }
    }

    public void Start()
    {
      //  Play(backgroundMusic);    
    }

    public void OnSceneLoad(string name)
    {
        if ((name == "MainMenu" || name == "OptionsMenu" || name == "CreditsScene"
    || name == "ControlsMenu" || name == "AlbumScene" || name == "Album2Scene") && !sounds[1].source.isPlaying)

        { Stop("2"); Stop("3"); Stop("4"); Stop("7"); Play("1"); }

        if (name == "LevelA1") { Stop("1"); Play("2"); }
        if (name == "LevelA2") { Stop("1"); Play("2"); }
        if (name == "LevelA3") { Stop("2"); Play("3"); }
        if (name == "LevelA4") { Stop("3"); Play("4"); }
        if (name == "LevelA5") { Play("4"); }
        if (name == "LevelB1") { Stop("4"); Play("5"); }
        if (name == "LevelB2") { Stop("5"); Play("7"); }
        if (name == "LevelB3") { Stop("5"); Play("7"); }
        
    }

    public void Play(string name)
    {
        var sound = Array.Find(sounds, sound => sound.name == name);
        if (sound == null)
        {
            Debug.LogWarning("Sound: " + name + " not found");
            return;
        }
        sound.source.Play();
    }

    public void Stop(string name)
    {
        var sound = Array.Find(sounds, sound => sound.name == name);
        if (sound == null)
        {
            Debug.LogWarning("Sound: " + name + " not found");
            return;
        }
        sound.source.Stop();
    }

    public float Volume
    {
        set
        {
            foreach (var sound in sounds)
            {

                sound.source.volume = value;
            }

        }
    }

}
