using UnityEngine;

//acá usamos herencia, por lo que tenemos un caso
//de ESPECIALIZACIÓN, ya que tanto la pelota del jugador
//como la del powerUp comparten lógica

public abstract class Ball : MonoBehaviour
{
    public float speed = 3;
    protected Vector3 Velocity;

    private void Start()
    {
        InitializeBall();
        LaunchBall();
    }

    protected virtual void InitializeBall()
    {
        
    }

    public abstract void LaunchBall();

    protected virtual void Update()
    {
        transform.position += Velocity * Time.deltaTime;
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {

        //acá tenemos un caso de reordenamiento
        //de código en base al EXPECTED PATH
        //ya que lo más probable es que choque con la paleta
        //y luego con los ladrillos

        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            float hitFactor = (transform.position.x - collision.transform.position.x) / collision.collider.bounds.size.x;
            
            Vector3 direction = new Vector3(hitFactor, 1, 0).normalized;
            Velocity = direction * speed;
        }
        
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Bricks"))
        {
            BounceBall(collision);
        }
        
        else
        {
            BounceBall(collision);
        }
    }

    private void BounceBall(Collision collision)
    {
        Vector3 normal = collision.contacts[0].normal;
        Velocity = Vector3.Reflect(Velocity, normal);
    }
}
