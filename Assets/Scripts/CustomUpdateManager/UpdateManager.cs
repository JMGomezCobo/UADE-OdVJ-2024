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
        List<ManagedUpdateBehaviour> updateListCopy = new List<ManagedUpdateBehaviour>(updateList);
        int count = updateListCopy.Count;
        for (int i = 0; i < count; i++)
            updateListCopy[i].UpdateMe();
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