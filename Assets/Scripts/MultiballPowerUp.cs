using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiballPowerUp : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            PaddleController paddleController = other.GetComponent<PaddleController>();
            Destroy(gameObject);
        }
        if (other.CompareTag("DeadZone")) Destroy(gameObject);
    }
}
