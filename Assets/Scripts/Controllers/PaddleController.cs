using UnityEngine;

public class PaddleController : MonoBehaviour
{
    public float speed = 10f;
    public float boundary = 8.75f;
    
    private Vector3 _startPosition;
    [SerializeField] private GameObject ballPrefab;
    
    private void Start()
    {
        _startPosition = transform.position;
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
        transform.position = _startPosition;
    }
}
