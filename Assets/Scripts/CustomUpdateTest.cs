using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomUpdateTest : MonoBehaviour
{
    private void OnEnable()
    {
        CustomUpdateManager.Instance.SubscribeToUpdate(MyUpdateFunction);
    }

    private void OnDisable()
    {
        CustomUpdateManager.Instance.UnsubscribeFromUpdate(MyUpdateFunction);
    }

    private void MyUpdateFunction()
    {
        Debug.Log("Hello!");
    }
}
