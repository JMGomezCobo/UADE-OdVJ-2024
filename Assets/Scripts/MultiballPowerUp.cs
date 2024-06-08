using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiballPowerUp : MonoBehaviour
{
    public GameObject Ball;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            Instantiate(Ball,this.position,Quaternion.identity);
            Destroy(gameObject);
        }
        if (other.CompareTag("DeadZone")) Destroy(gameObject);
    }
}
