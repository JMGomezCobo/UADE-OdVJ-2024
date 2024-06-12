using UnityEngine;

//acá usamos herencia, por lo que tenemos un caso
//de ESPECIALIZACIÓN, ya que tanto la pelota del jugador
//como la del powerUp comparten lógica

public class MultiBallController : Ball
{
    protected override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("DeadZone"))
        {
            ObjectPool.Instance.ReturnObject(gameObject);
        }
        
        else
        {
            base.OnCollisionEnter(collision);
        }
    }

    public override void LaunchBall()
    {
        _velocity = new Vector3(Random.Range(-1, 1f), 1, 0).normalized * speed;
    }
}