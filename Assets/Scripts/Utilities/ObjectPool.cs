using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;
    public GameObject ballPrefab;
    public int poolSize = 10;

    private readonly Queue<GameObject> _pool = new();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            InitializePool();
        }
        
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(ballPrefab);
            obj.SetActive(false);
            
            _pool.Enqueue(obj);
        }
    }

    public GameObject GetObject()
    {
        if (_pool.Count > 0)
        {
            GameObject obj = _pool.Dequeue();
            obj.SetActive(true);
            
            return obj;
        }
        
        else
        {
            GameObject obj = Instantiate(ballPrefab);
            obj.SetActive(true);
            
            return obj;
        }
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        _pool.Enqueue(obj);
    }
}