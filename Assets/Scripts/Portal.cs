using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private GameObject winScreen;

    private AudioManager audioManager;
    private CoinManager coinManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        coinManager = FindFirstObjectByType<CoinManager>();

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        int playerLayer = LayerMask.NameToLayer("Player");
        if (other.gameObject.layer == playerLayer)
        {
            if (coinManager.reachedCoinGoal)
            {
                audioManager.Stop();
                audioManager.PlaySFX(audioManager.WinSfx);
                winScreen.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }
}
