using UnityEngine;

public class ManagedUpdateBehaviour : MonoBehaviour
{
    protected virtual void OnEnable()
    {
        UpdateManager.Instance.AddToUpdateList(this);
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