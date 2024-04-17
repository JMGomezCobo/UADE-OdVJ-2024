using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float inputValue;
    public float moveSpeed = 25f;
    public float maxSpeed = 25f;
    public float maxXPosition = 10f;
    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
    }

    void Update()
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

}
