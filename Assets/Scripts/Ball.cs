using UnityEngine;
using System.Collections;

public abstract class Ball : MonoBehaviour
{
    public float speed = 3;
    protected Vector3 _velocity;
    public int baseDamage = 1;
    public float damageMultiplier = 1.0f;

    private void Start()
    {
        InitializeBall();
        LaunchBall();
    }

    protected virtual void InitializeBall()
    {
        // This method can be overridden by derived classes for specific initialization
    }

    public abstract void LaunchBall();

    protected virtual void Update()
    {
        transform.position += _velocity * Time.deltaTime;
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            float hitFactor = (transform.position.x - collision.transform.position.x) / collision.collider.bounds.size.x;
            Vector3 direction = new Vector3(hitFactor, 1, 0).normalized;
            _velocity = direction * speed;
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Bricks"))
        {
            Vector3 normal = collision.contacts[0].normal;
            _velocity = Vector3.Reflect(_velocity, normal);
            ApplyDamage(collision.gameObject);
        }
        else
        {
            Vector3 normal = collision.contacts[0].normal;
            _velocity = Vector3.Reflect(_velocity, normal);
        }
    }

    protected void ApplyDamage(GameObject brick)
    {
        var brickController = brick.GetComponent<BrickController>();
        if (brickController != null)
        {
            int damage = Mathf.CeilToInt(baseDamage * damageMultiplier);
            brickController.TakeDamage(damage);
        }
    }

    public void IncreaseDamageTemporarily(float amount, float duration)
    {
        StartCoroutine(TemporaryDamageIncrease(amount, duration));
    }

    private IEnumerator TemporaryDamageIncrease(float amount, float duration)
    {
        damageMultiplier += amount;
        yield return new WaitForSeconds(duration);
        damageMultiplier -= amount;
    }
}


