using Managers;
using UnityEngine;

public class BrickController : MonoBehaviour
{
    public BrickData brickData;
    private int _currentHits;

    private void Start()
    {
        _currentHits = brickData.hitsToDestroy;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Ball")) return;

        var ball = collision.gameObject.GetComponent<Ball>();
        
        if (ball != null)
        {
            TakeDamage(ball.baseDamage);
        }
    }

    public void TakeDamage(int damage)
    {
        _currentHits -= damage;

        if (_currentHits <= 0)
        {
            DestroyBrick();
        }
    }

    private void DestroyBrick()
    {
        GameManager.Instance.BrickDestroyed();
        GameManager.Instance.AddScore(brickData.pointValue);

        TrySpawnPowerUp();
        Destroy(gameObject);
    }

    private void TrySpawnPowerUp()
    {
        if (Random.value < brickData.powerUpChance)
        {
            Instantiate(brickData.powerUpPrefab, transform.position, Quaternion.identity);
        }
    }
}