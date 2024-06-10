using Managers;
using UnityEngine;
using UnityEngine.Events;

public class BrickController : MonoBehaviour
{
    public BrickData brickData;

    public UnityEvent<int> onBrickDestroyed;
    private int _currentHits;

    private void Start()
    {
        _currentHits = brickData.hitsToDestroy;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Ball")) return;
        
        _currentHits--;

        if (_currentHits <= 0)
        {
            DestroyBrick();
        }
    }

    private void DestroyBrick()
    {
        onBrickDestroyed.Invoke(brickData.pointValue);
        LevelManager.Instance.BrickDestroyed();
        
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