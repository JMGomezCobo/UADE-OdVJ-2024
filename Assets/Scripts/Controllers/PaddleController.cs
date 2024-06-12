using Managers;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    public float speed = 10f;
    public float boundary = 8.75f;
    
    private Vector3 _startPosition;
    [SerializeField] private GameObject ballPrefab;
    
    private void Start()
    {
        //acá tenemos un caso de PreComputation
        //ya que más adelante vamos 
        //a utilizar el valor de _startPosition

        _startPosition = transform.position;
    }

    private void OnEnable()
    {
        CustomUpdateManager.Instance.SubscribeToUpdate(HandleMovement);
    }

    private void HandleMovement()
    {
        //acá hay un caso de LazyComputation
        //ya que no realizamos este cálculo
        //hasta que la paleta no se mueva

        float input = Input.GetAxis("Horizontal");
        
        Vector3 newPosition = transform.position + Vector3.right * input * speed * Time.deltaTime;
        newPosition.x = Mathf.Clamp(newPosition.x, -boundary, boundary);
        
        transform.position = newPosition;
    }
    
    public void ResetPlayer()
    {
        //acá encontramos otro caso de LazyComputation
        //ya que este cálculo lo realizamos simplemente
        //cuando es necesario resetear la posición del jugador
        //además acá estamos usando un valor que previamente 
        //calculamos mediante PreComputation

        transform.position = _startPosition;
    }
}
