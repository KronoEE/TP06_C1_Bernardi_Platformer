using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPowerUp : MonoBehaviour
{
   [SerializeField] private int healthToAdd = 2;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        int playerLayer = LayerMask.NameToLayer("Player");

        if (collision.gameObject.layer == playerLayer)
        {
            PlayerController playerScript = collision.gameObject.GetComponent<PlayerController>();
            playerScript.Addhealth(healthToAdd);
            Destroy(gameObject);
        }
    }
}
