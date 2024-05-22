using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : ManagedUpdateBehaviour
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

    public override void UpdateMe()
    {
        float inputHorizontal = Input.GetAxisRaw("Horizontal");
        MovePlayer(inputHorizontal);
    }
    public void ResetPlayer()
    {
        transform.position = startPosition;
    }
    void MovePlayer(float inputHorizontal)
    {
        float movementX = inputHorizontal * moveSpeed * Time.deltaTime;
        float newXPosition = Mathf.Clamp(transform.position.x + movementX, - maxXPosition, maxXPosition);
        transform.position = new Vector3(newXPosition, transform.position.y, transform.position.z);
    }
    public void LaunchMultipleBallsFromPlayer(int count)
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
