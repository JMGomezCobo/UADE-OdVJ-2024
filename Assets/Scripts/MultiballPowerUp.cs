using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiballPowerUp : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            Player player = other.GetComponent<Player>();
            if (player != null && player.ballPrefab != null) player.SpawnMultipleBalls(2);
            Destroy(gameObject);
        }
    }
  
}
