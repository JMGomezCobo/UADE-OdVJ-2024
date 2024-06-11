using UnityEngine;

//ac� usamos herencia, por lo que tenemos un caso
//de ESPECIALIZACI�N, ya que tanto la pelota del jugador
//como la del powerUp comparten l�gica

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

        //ac� tenemos un caso de reordenamiento
        //de c�digo en base al EXPECTED PATH
        //ya que lo m�s probable es que choque con la paleta
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
