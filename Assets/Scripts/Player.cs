using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Player : MonoBehaviour
{
    private float inputValue;
    public float moveSpeed = 25f;
    public float maxSpeed = 25f;
    public float maxXPosition = 10f;
    private Vector3 startPosition;
    [SerializeField] GameObject ballPrefab;
    
    private void Start()
    {
        startPosition = transform.position;
    }

    private void OnEnable()
    {
        CustomUpdateManager.Instance.SubscribeToUpdate(HandleMovement);
    }
    
    public void HandleMovement()
    {
        float inputHorizontal = Input.GetAxisRaw("Horizontal");
        MovePlayer(inputHorizontal);
    }
    
    public void ResetPlayer()
    {
        transform.position = startPosition;
    }

    private void MovePlayer(float inputHorizontal)
    {
        float movementX = inputHorizontal * moveSpeed * Time.deltaTime;
        var position = transform.position;
        float newXPosition = Mathf.Clamp(position.x + movementX, - maxXPosition, maxXPosition);
        
        position = new Vector3(newXPosition, position.y, position.z);
        transform.position = position;
    }

    private void LaunchMultipleBallsFromPlayer(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject newBall = Instantiate(ballPrefab, transform.position, Quaternion.identity);
            newBall.GetComponent<Ball>().LaunchBall();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp")) LaunchMultipleBallsFromPlayer(2);
    }
}
