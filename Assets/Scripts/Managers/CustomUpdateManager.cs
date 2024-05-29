using System;
using System.Collections.Generic;
using UnityEngine;

public class CustomUpdateManager : MonoBehaviour
{
    private static CustomUpdateManager _instance;
    
    public static CustomUpdateManager Instance
    {
        get
        {
            if (_instance != null) return _instance;
            
            _instance = FindObjectOfType<CustomUpdateManager>();

            if (_instance != null) return _instance;
            
            GameObject managerObject = new GameObject("CustomUpdateManager");
            _instance = managerObject.AddComponent<CustomUpdateManager>();

            return _instance;
        }
    }

    #region Actions Lists (3)

    private readonly List<Action> _updateActions = new();
    private readonly List<Action> _fixedUpdateActions = new();
    private readonly List<Action> _lateUpdateActions = new();

    #endregion
    
    #region Subscribe Methods (3)

    /// <summary>
    /// Subscribes a method to be called during the Unity Update loop.
    /// </summary>
    /// <param name="action"></param>
    public void SubscribeToUpdate(Action action)
    {
        _updateActions.Add(action);
    }

    /// <summary>
    /// Subscribes a method to be called during the Unity FixedUpdate loop.
    /// </summary>
    /// <param name="action"></param>
    public void SubscribeToFixedUpdate(Action action)
    {
        _fixedUpdateActions.Add(action);
    }

    /// <summary>
    /// Subscribes a method to be called during the Unity LateUpdate loop.
    /// </summary>
    /// <param name="action"></param>
    public void SubscribeToLateUpdate(Action action)
    {
        _lateUpdateActions.Add(action);
    }

    #endregion

    #region Unsuscribe Methods (3)

    /// <summary>
    /// Unsubscribes a method from being called during the Unity Update loop.
    /// </summary>
    /// <param name="action"></param>
    public void UnsubscribeFromUpdate(Action action)
    {
        _updateActions.Remove(action);
    }

    /// <summary>
    /// Unsubscribes a method from being called during the Unity FixedUpdate loop.
    /// </summary>
    /// <param name="action"></param>
    public void UnsubscribeFromFixedUpdate(Action action)
    {
        _fixedUpdateActions.Remove(action);
    }

    /// <summary>
    /// Unsubscribes a method from being called during the Unity LateUpdate loop.
    /// </summary>
    /// <param name="action"></param>
    public void UnsubscribeFromLateUpdate(Action action)
    {
        _lateUpdateActions.Remove(action);
    }

    #endregion

    #region Unity Built-In Methods (3)

    private void Update()
    {
        foreach (var action in _updateActions)
        {
            action?.Invoke();
        }
    }

    private void FixedUpdate()
    {
        foreach (var action in _fixedUpdateActions)
        {
            action?.Invoke();
        }
    }

    private void LateUpdate()
    {
        foreach (var action in _lateUpdateActions)
        {
            action?.Invoke();
        }
    }

    #endregion
}