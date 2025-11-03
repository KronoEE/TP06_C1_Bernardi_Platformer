using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalSound : MonoBehaviour
{
    private AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        int playerLayer = LayerMask.NameToLayer("Player");
        if (other.gameObject.layer == playerLayer)
        {
                audioManager.PlayLoopedSfx(audioManager.portalSfx);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        int playerLayer = LayerMask.NameToLayer("Player");
        if (other.gameObject.layer == playerLayer)
        {
            if (audioManager != null)
            {
                audioManager.Stop();
            }
        }
    }
}
