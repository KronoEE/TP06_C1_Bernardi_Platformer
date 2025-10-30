using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider UISlider;
    private void Start()
    {
        SetMusicVolume();
        SetSFXVolume();
        SetUiSlider();
    }
    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        audioMixer.SetFloat("music", Mathf.Log10(volume) * 20);
    }

     public void SetSFXVolume()
    {
        float volume = sfxSlider.value;
        audioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
    }

    public void SetUiSlider()
    {
        float volume = UISlider.value;NO 
        audioMixer.SetFloat("UI", Mathf.Log10(volume) * 20);
    }
}
