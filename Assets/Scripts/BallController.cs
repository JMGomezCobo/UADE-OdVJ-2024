using Managers;
using UnityEngine;

public class BallController : Ball
{
    public static BallController Instance;
    
    private Transform _playerTransform;
    private LevelManager _gameManager;

    [SerializeField] private float speedMultiplier = 1.0125f;
    [SerializeField] private float maxSpeed = 1.5f;
    
    private bool _readyToLaunch = true;

    public new void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    protected override void InitializeBall()
    {
        _gameManager = LevelManager.Instance;
        PaddleController paddleController = FindObjectOfType<PaddleController>();

        if (paddleController == null) return;
        
        _playerTransform = paddleController.transform;
        ResetBall();
    }

    private void OnEnable()
    {
        CustomUpdateManager.Instance.SubscribeToUpdate(UpdateBall);
    }

    private void UpdateBall()
    {
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
        
        base.Update();
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("DeadZone"))
        {
            _gameManager.LoseLife();
            ResetBall();
            
            _readyToLaunch = true;
        }
        
        else
        {
            IncreaseSpeed();
            base.OnCollisionEnter(collision);
        }
    }

    public void ResetBall()
    {
        PaddleController paddleController = FindObjectOfType<PaddleController>();

        if (paddleController == null) return;

        var playerPosition = paddleController.transform.position;

        transform.position = new Vector3(playerPosition.x, playerPosition.y + 0.5f, 0);
        Velocity = Vector3.zero;
    }

    public override void LaunchBall()
    {
        Velocity = new Vector3(Random.Range(-1, 1f), 1, 0).normalized * speed;
    }
    
    private void IncreaseSpeed()
    {
        Velocity *= speedMultiplier;
        
        if (Velocity.magnitude > maxSpeed)
        {
            Velocity = Velocity.normalized * maxSpeed;
        }
    }
}