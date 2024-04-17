using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
   private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball")) 
        {
            FindObjectOfType<GameManager>().CheckLevelCompleted();
            //stroy(gameObject); 
            gameObject.SetActive(false);
        }
    }
}
