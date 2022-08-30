
using UnityEngine;

public class AudioSettings : MonoBehaviour
{

    private static readonly string MusicPref = "MusicPref";
    private static readonly string SoundEffectsPref = "SoundEffectsPref";
    private float musicFloat, soundEffectsFloat;
    public AudioSource musicAudio;
    public AudioSource[] soundEffectsAudio;

    void Awake()
    {
        ContinueSettings();
    }

    private void ContinueSettings()
    {
        musicFloat = PlayerPrefs.GetFloat(MusicPref);
        soundEffectsFloat = PlayerPrefs.GetFloat(SoundEffectsPref);

        musicAudio.volume = musicFloat;
        for (int i = 0; i < soundEffectsAudio.Length; i++)
        {
            soundEffectsAudio[i].volume = soundEffectsFloat;
        }
    }

}
