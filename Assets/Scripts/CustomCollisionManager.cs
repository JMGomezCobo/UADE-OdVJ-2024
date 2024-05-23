using System;
using System.Collections.Generic;
using UnityEngine;

public class CustomCollisionManager : MonoBehaviour
{
    private static CustomCollisionManager _instance;
    
    public static CustomCollisionManager Instance
    {
        get
        {
            if (_instance != null) return _instance;
            
            _instance = FindObjectOfType<CustomCollisionManager>();

            if (_instance != null) return _instance;
                
            GameObject managerObject = new GameObject("CustomCollisionManager");
            
            _instance = managerObject.AddComponent<CustomCollisionManager>();
            
            return _instance;
        }
    }

    private readonly List<Action<Collision>> _collisionEnterActions = new();
    private readonly List<Action<Collision>> _collisionExitActions = new();
    private readonly List<Action<Collision>> _collisionStayActions = new();

    public void SubscribeToCollisionEnter(Action<Collision> action)
    {
        _collisionEnterActions.Add(action);
    }

    public void SubscribeToCollisionExit(Action<Collision> action)
    {
        _collisionExitActions.Add(action);
    }

    public void SubscribeToCollisionStay(Action<Collision> action)
    {
        _collisionStayActions.Add(action);
    }

    public void UnsubscribeFromCollisionEnter(Action<Collision> action)
    {
        _collisionEnterActions.Remove(action);
    }

    public void UnsubscribeFromCollisionExit(Action<Collision> action)
    {
        _collisionExitActions.Remove(action);
    }

    public void UnsubscribeFromCollisionStay(Action<Collision> action)
    {
        _collisionStayActions.Remove(action);
    }

    private void OnCollisionEnter(Collision collision)
    {
        foreach (var action in _collisionEnterActions)
        {
            action?.Invoke(collision);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        foreach (var action in _collisionExitActions)
        {
            action?.Invoke(collision);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        foreach (var action in _collisionStayActions)
        {
            action?.Invoke(collision);
        }
    }
}

