using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField] GameObject multiballPowerUpPrefab;
   private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball")) 
        {
            FindObjectOfType<GameManager>().CheckLevelCompleted();
            //Destroy(gameObject);
            gameObject.SetActive(false);
            if (Random.Range(0f, 1f) < 0.2f) Instantiate(multiballPowerUpPrefab, transform.position, Quaternion.identity);
        }
    }
}
