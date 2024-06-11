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
        //ac� tenemos un caso de PreComputation
        //ya que m�s adelante vamos 
        //a utilizar el valor de _startPosition

        _startPosition = transform.position;
    }

    private void OnEnable()
    {
        CustomUpdateManager.Instance.SubscribeToUpdate(HandleMovement);
    }

    private void HandleMovement()
    {
        //ac� hay un caso de LazyComputation
        //ya que no realizamos este c�lculo
        //hasta que la paleta no se mueva

        float input = Input.GetAxis("Horizontal");
        
        Vector3 newPosition = transform.position + Vector3.right * input * speed * Time.deltaTime;
        newPosition.x = Mathf.Clamp(newPosition.x, -boundary, boundary);
        
        transform.position = newPosition;
    }
    
    public void ResetPlayer()
    {
        //ac� encontramos otro caso de LazyComputation
        //ya que este c�lculo lo realizamos simplemente
        //cuando es necesario resetear la posici�n del jugador
        //adem�s ac� estamos usando un valor que previamente 
        //calculamos mediante PreComputation

        transform.position = _startPosition;
    }
}
