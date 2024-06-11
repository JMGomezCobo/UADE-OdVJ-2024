using Managers;
using UnityEngine;

public class LifePickup : MonoBehaviour
{
    public int lifeAmount = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        
        GameManager.Instance.AddLife(lifeAmount);
        Destroy(gameObject);
    }
}

