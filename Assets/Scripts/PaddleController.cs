using Managers;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    public float speed = 10f;
    public float boundary = 9f;
    
    private Vector3 startPosition;
    [SerializeField] GameObject ballPrefab;
    
    private void Start()
    {
        startPosition = transform.position;
    }

    private void OnEnable()
    {
        CustomUpdateManager.Instance.SubscribeToUpdate(HandleMovement);
    }

    private void HandleMovement()
    {
        float input = Input.GetAxis("Horizontal");
        
        Vector3 newPosition = transform.position + Vector3.right * input * speed * Time.deltaTime;
        newPosition.x = Mathf.Clamp(newPosition.x, -boundary, boundary);
        
        transform.position = newPosition;
    }
    
    public void ResetPlayer()
    {
        transform.position = startPosition;
    }
}
