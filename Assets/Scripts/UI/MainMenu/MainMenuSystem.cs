using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuSystem : MonoBehaviour
{
    [SerializeField] private Button playBtn;
    [SerializeField] private Button SettingsBtn;
    [SerializeField] private Button quitBtn;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private Button SettingsBackBtn;

    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        playBtn.onClick.AddListener(OnPlayClicked);
        quitBtn.onClick.AddListener(OnQuitClicked);
        SettingsBtn.onClick.AddListener(OnSettingsClicked);
        SettingsBackBtn.onClick.AddListener(OnSettingsBackButton);
    }
    private void OnDestroy()
    {
        playBtn.onClick.RemoveAllListeners();
        quitBtn.onClick.RemoveAllListeners();
        SettingsBtn.onClick.RemoveAllListeners();
        SettingsBackBtn.onClick.RemoveAllListeners();
    }
   private void OnPlayClicked()
    {
        audioManager.PlayUI(audioManager.ButtonUI);
        SceneManager.LoadScene("Level_01");
        Time.timeScale = 1;
    }

    private void OnQuitClicked()
    {
        audioManager.PlayUI(audioManager.ButtonUI);
        Application.Quit();
    }

    private void OnSettingsClicked()
    {
        audioManager.PlayUI(audioManager.ButtonUI);
        settingsPanel.SetActive(true);
    }

    private void OnSettingsBackButton()
    {
        audioManager.PlayUI(audioManager.ButtonUI);
        settingsPanel.SetActive(false);
    }
}
