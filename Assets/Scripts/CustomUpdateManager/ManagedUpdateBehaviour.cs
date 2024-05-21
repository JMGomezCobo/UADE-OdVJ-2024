using UnityEngine;

public class ManagedUpdateBehaviour : MonoBehaviour
{
    protected virtual void OnEnable()
    {
        if (UpdateManager.Instance != null) UpdateManager.Instance.AddToUpdateList(this);
        else Debug.LogError("UpdateManager instance es nulo");
    }
    protected virtual void OnDisable()
    {
        UpdateManager.Instance.RemoveFromUpdateList(this);
    }
    public virtual void UpdateMe()
    {
        //Este método debe ser sobreescrito por las clases derivadas
    }
}