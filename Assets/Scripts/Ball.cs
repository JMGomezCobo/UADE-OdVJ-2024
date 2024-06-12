using System.Collections;
using UnityEngine;

public abstract class Ball : MonoBehaviour
{
    public float speed = 3;
    protected Vector3 _velocity;
    public int baseDamage = 1;
    public float damageMultiplier = 1.0f;

    [Header("Audio Clips")]
    public AudioClip hitPlayerClip;
    public AudioClip hitBrickClip;
    public AudioClip hitWallClip;
    
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        
        InitializeBall();
        LaunchBall();
    }

    protected virtual void InitializeBall()
    {
        
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

            PlaySound(hitPlayerClip);
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Bricks"))
        {
            Vector3 normal = collision.contacts[0].normal;
            _velocity = Vector3.Reflect(_velocity, normal);
            ApplyDamage(collision.gameObject);

            PlaySound(hitBrickClip);
        }
        else
        {
            Vector3 normal = collision.contacts[0].normal;
            _velocity = Vector3.Reflect(_velocity, normal);

            PlaySound(hitWallClip);
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

    private void PlaySound(AudioClip clip)
    {
        if (clip != null && _audioSource != null)
        {
            _audioSource.PlayOneShot(clip);
        }
    }

    public void IncreaseDamageTemporarily(float amount, float duration)
    {
        StartCoroutine(TemporaryDamageIncrease(amount, duration));
    }

    public IEnumerator TemporaryDamageIncrease(float amount, float duration)
    {
        damageMultiplier += amount;
        yield return new WaitForSeconds(duration);
        damageMultiplier -= amount;
    }
}



