using UnityEngine;

//acá usamos herencia, por lo que tenemos un caso
//de ESPECIALIZACIÓN, ya que tanto la pelota del jugador
//como la del powerUp comparten lógica

public class MultiBallController : Ball
{
   private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("DeadZone"))
        {
            ObjectPool.Instance.ReturnObject(gameObject); // Return the ball to the pool
        }
        
        else
        {
            base.OnCollisionEnter(collision);
        }
    }

    public override void LaunchBall()
    {
        Velocity = new Vector3(Random.Range(-1, 1f), 1, 0).normalized * speed;
    }
}