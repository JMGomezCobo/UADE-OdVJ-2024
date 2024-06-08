using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiballPowerUp : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            BallController ballController = FindObjectOfType<BallController>();
            if (ballController != null) ballController.LaunchMultipleBalls(transform.position, 2);
            Destroy(gameObject);
        }
        if (other.CompareTag("DeadZone")) Destroy(gameObject);
    }
}
