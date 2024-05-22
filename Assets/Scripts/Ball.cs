using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : ManagedUpdateBehaviour
{
    public float speed = 3;
    private Vector3 velocity;
    private Transform playerTransform;
    Vector3 startPosition;
    GameManager gameManager;
    public static Ball Instance;
    private bool readyToLaunch = true;

    public void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    void Start()
    {
        gameManager = GameManager.Instance;
        Player player = FindObjectOfType<Player>();
        if (player != null)
        {
            playerTransform = FindObjectOfType<Player>().transform;
            ResetBall();
        }
        else Debug.LogError("No se encontro player");
    }
    public override void UpdateMe()
    {
        if (readyToLaunch) transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y + 0.5f, 0);
        if (Input.GetKeyDown(KeyCode.Space) && readyToLaunch)
        {
            LaunchBall();
            readyToLaunch = false;
        }
        if (!readyToLaunch) transform.position += velocity * Time.deltaTime;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("DeadZone")) 
        { 
            gameManager.LoseHealth();
            ResetBall();
            readyToLaunch=true;
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            float hitFactor = (transform.position.x - collision.transform.position.x) / collision.collider.bounds.size.x;
            Vector3 direction = new Vector3(hitFactor, 1, 0).normalized;
            velocity = direction * speed;
        }
        else if (collision.gameObject.CompareTag("Brick"))
        {
            Vector3 normal = collision.contacts[0].normal;
            velocity = Vector3.Reflect(velocity, normal);
        }
        else
        {
            Vector3 normal = collision.contacts[0].normal;
            velocity = Vector3.Reflect(velocity, normal);
        }
    }
    public void ResetBall()
    {
        Player player = FindObjectOfType<Player>();
        if (player != null)
        {
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 0.5f, 0);
            velocity = Vector3.zero;
        }
        else Debug.LogError("No se encontro player");

    }
    public void LaunchBall()
    {
           velocity = new Vector3(Random.Range(-1, 1f), 1, 0).normalized * speed;
    }
    public void LaunchMultipleBalls(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 offset = new Vector3(Random.Range(-0.5f, 0.5f), 0, 0);
            Vector3 newPosition = playerTransform.position + offset;
            GameObject newBall = Instantiate(gameObject, newPosition, Quaternion.identity);
            newBall.GetComponent<Ball>().LaunchBall();
        }
    }
}
