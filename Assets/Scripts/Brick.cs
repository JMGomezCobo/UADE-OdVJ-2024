using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField] GameObject multiballPowerUpPrefab;
    private bool HasLife;
    public int Vida;

    private void OnCollisionEnter(Collision collision)
    {
       if (collision.gameObject.CompareTag("Ball")) 
       {
          Vida -= 1;
          if (Vida == 0) HasLife = false;
          if (!HasLife)
          {
             FindObjectOfType<GameManager>().CheckLevelCompleted();
             gameObject.SetActive(false);
             if (Random.Range(0f, 1f) < 0.2f) Instantiate(multiballPowerUpPrefab, transform.position, Quaternion.identity);
          }
       }
    }
}
