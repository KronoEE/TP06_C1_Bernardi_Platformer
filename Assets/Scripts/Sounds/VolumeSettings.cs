using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider UISlider;
    private void Awake()
    {
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
        UISlider.onValueChanged.AddListener(SetUiVolume);
    }
    private void Start()
    {
        SetMusicVolume(musicSlider.value);
        SetSFXVolume(sfxSlider.value);
        SetUiVolume(UISlider.value);
    }

    public void SetMusicVolume(float volume)
    {
        volume = musicSlider.value;
        audioMixer.SetFloat("music", Mathf.Log10(volume) * 20);
    }

     public void SetSFXVolume(float volume)
    {
        volume = sfxSlider.value;
        audioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
    }

    public void SetUiVolume(float volume)
    {
        volume = UISlider.value; 
        audioMixer.SetFloat("UI", Mathf.Log10(volume) * 20);
    }
}
