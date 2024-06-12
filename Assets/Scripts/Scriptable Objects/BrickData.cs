using UnityEngine;

[CreateAssetMenu(fileName = "BrickData", menuName = "ScriptableObjects/BrickData", order = 1)]
public class BrickData : ScriptableObject
{
    public int pointValue = 100;
    public int hitsToDestroy = 1;
    [Range(0, 1)] public float powerUpChance = 0.5f;
    
    public GameObject powerUpPrefab;
}

