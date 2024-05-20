using System;
using System.Collections.Generic;
using UnityEngine;

public class UpdateManager : MonoBehaviour
{
    public static UpdateManager Instance;
    private List<ManagedUpdateBehaviour> updateList = new List<ManagedUpdateBehaviour>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    private void Update()
    {
        int count = updateList.Count;
        for (int i = 0; i < count; i++)
            updateList[i].UpdateMe();
    }
    public void AddToUpdateList(ManagedUpdateBehaviour component)
    {
        if (!updateList.Contains(component)) updateList.Add(component);
    }
    public void RemoveFromUpdateList(ManagedUpdateBehaviour component)
    {
        updateList.Remove(component);
    }
}