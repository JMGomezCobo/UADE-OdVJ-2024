using UnityEngine;

namespace Power_Ups
{
    public class MultiBallPowerUp : MonoBehaviour
    {
        public int numberOfBallsToSpawn = 3;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
        
            SpawnBalls();
            Destroy(gameObject);
        }

        private void SpawnBalls()
        {
            Transform playerTransform = FindObjectOfType<PaddleController>().transform;
        
            for (int i = 0; i < numberOfBallsToSpawn; i++)
            {
                GameObject newBall = ObjectPool.Instance.GetObject();
                newBall.transform.position = playerTransform.position + (Vector3.up * 0.5f);
            
                MultiBallController ballController = newBall.GetComponent<MultiBallController>();
                ballController.LaunchBall();
            }
        }
    }
}