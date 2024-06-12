using UnityEngine;

public class DamagePowerUp : MonoBehaviour
{
    public float damageIncreaseAmount = 0.5f;
    public float duration = 5.0f;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        
        ApplyPowerUp();
        Destroy(gameObject);
    }

    private void ApplyPowerUp()
    {
        var balls = FindObjectsOfType<Ball>();
        
        foreach (var ball in balls)
        {
            ball.IncreaseDamageTemporarily(damageIncreaseAmount, duration);
        }
    }
}

