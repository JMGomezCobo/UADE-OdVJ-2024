using Managers;
using Unity.VisualScripting;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public float speed = 3;
    private Vector3 _velocity;
    private Transform _playerTransform;
    Vector3 _startPosition;
    GameManager _gameManager;
    public static BallController Instance;
    private bool _readyToLaunch = true;

    public void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        _gameManager = GameManager.Instance;
        PaddleController paddleController = FindObjectOfType<PaddleController>();
        
        //acá tenemos un caso de CACHING ya que nos
        //estamos guardando el resultado de una operación muy costosa
        //para utilizarlo más adelante

        if (paddleController != null)
        {
            _playerTransform = FindObjectOfType<PaddleController>().transform;
            ResetBall();
        }
    }
    
    private void OnEnable()
    {
        CustomUpdateManager.Instance.SubscribeToUpdate(UpdateBall);
    }
    
    public void UpdateBall()
    {
        //acá hay varios ejemplos de precomputation
        //donde hacemos cálculos dentro de condicionales
        //para usar los resultados más adelante.

        if (_readyToLaunch)
        {
            var playerPosition = _playerTransform.position;
            transform.position = new Vector3(playerPosition.x, playerPosition.y + 0.5f, 0);
        }

        if (Input.GetKeyDown(KeyCode.Space) && _readyToLaunch)
        {
            LaunchBall();
            _readyToLaunch = false;
        }
        
        if (!_readyToLaunch) transform.position += _velocity * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //acá tenemos un caso de reordenamiento de código
        //en base al EXPECTED PATH.
        //Estamos suponiendo que el procesamiento de datos
        //más probable va a ser que la pelota primero choque
        //con los ladrillos, luego con la paleta y finalmente las dead zones

        if (collision.gameObject.layer == LayerMask.NameToLayer("Bricks"))
        {
            Vector3 normal = collision.contacts[0].normal;
            _velocity = Vector3.Reflect(_velocity, normal);
        }

        else if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            float hitFactor = (transform.position.x - collision.transform.position.x) / collision.collider.bounds.size.x;
            Vector3 direction = new Vector3(hitFactor, 1, 0).normalized;
            _velocity = direction * speed;
        }
        
        else if (collision.gameObject.layer == LayerMask.NameToLayer("DeadZone"))
        { 
            _gameManager.LoseHealth();
            ResetBall();
            _readyToLaunch = true;
        }

        else
        {
            Vector3 normal = collision.contacts[0].normal;
            _velocity = Vector3.Reflect(_velocity, normal);
        }
    }

    public void ResetBall()
    {
        PaddleController paddleController = FindObjectOfType<PaddleController>();

        if (paddleController == null) return;
        
        var playerPosition = paddleController.transform.position;
            
        transform.position = new Vector3(playerPosition.x, playerPosition.y + 0.5f, 0);
        _velocity = Vector3.zero;
    }
    
    public void LaunchBall()
    {
        _velocity = new Vector3(Random.Range(-1, 1f), 1, 0).normalized * speed;
    }
}
