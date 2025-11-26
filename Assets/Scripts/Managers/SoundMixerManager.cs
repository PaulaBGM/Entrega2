using UnityEngine;
using UnityEngine.Audio;

public class SoundMixerManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    public void SetMasterVolume(float volume) 
    {
        audioMixer.SetFloat("masterVolume", volume);
    }
    public void SetFXVolume(float volume)
    {
        audioMixer.SetFloat("sfxVolume", volume);
    }
    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("musicVolume", volume);
    }
}
