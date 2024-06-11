using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed = 3;
    private Vector3 velocity;
    Vector3 startPosition;
    GameManager gameManager;
    public static Ball Instance;

    public void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        //acá tenemos un ejemplo de precomputation
        //ya que estamos realizando operaciones previo
        // a usar los valores que asignamos
        gameManager = GameManager.Instance;
        startPosition = transform.position;
        ResetBall();
    }

    void Update()
    {
        if (gameManager.lives > 0) transform.position += velocity * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //acá tenemos un caso de "expected path"
        //puesto que, en orden de prioridad,
        //esperamos que el output más común sea colisionar con los ladrillos
        //luego con la paleta,
        //finalmente las dead zones

        if (collision.gameObject.CompareTag("Brick"))
        {
            Vector3 normal = collision.contacts[0].normal;
            velocity = Vector3.Reflect(velocity, normal);
        }

        else if (collision.gameObject.CompareTag("Player"))
        {
            float hitFactor = (transform.position.x - collision.transform.position.x) / collision.collider.bounds.size.x;
            Vector3 direction = new Vector3(hitFactor, 1, 0).normalized;
            velocity = direction * speed;
        }

        else if (collision.gameObject.CompareTag("DeadZone")) 
        { 
            gameManager.LoseHealth();
            ResetBall();
        }

        else
        {
            Vector3 normal = collision.contacts[0].normal;
            velocity = Vector3.Reflect(velocity, normal);
        }
    }

    public void ResetBall()
    {
        transform.position = startPosition;
        velocity = new Vector3(Random.Range(-1.1f,1.1f),1,0).normalized * speed;
    }
}
