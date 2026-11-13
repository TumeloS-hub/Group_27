using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider musicSlider;
    public Slider sfxSlider;

    private void Start()
    {
        if (musicSlider != null)
        {
            float savedMusic = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
            musicSlider.value = savedMusic;
            SetMusicVolume(savedMusic);
        }

        if (sfxSlider != null)
        {
            float savedSFX = PlayerPrefs.GetFloat("SFXVolume", 0.75f);
            sfxSlider.value = savedSFX;
            SetSFXVolume(savedSFX);
        }
    }

    public void SetMusicVolume(float sliderValue)
    {
        float volume = Mathf.Log10(Mathf.Clamp(sliderValue, 0.0001f, 1f)) * 20;
        audioMixer.SetFloat("MusicVol", volume);
        PlayerPrefs.SetFloat("MusicVolume", sliderValue);
    }

    public void SetSFXVolume(float sliderValue)
    {
        float volume = Mathf.Log10(Mathf.Clamp(sliderValue, 0.0001f, 1f)) * 20;
        audioMixer.SetFloat("SFXVol", volume);
        PlayerPrefs.SetFloat("SFXVolume", sliderValue);
    }
}