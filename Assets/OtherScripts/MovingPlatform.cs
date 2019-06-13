using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector3 startDirection;
    public float turnInterval;
    public float initialDelay;
    public float speed;

    private float direction = 1;
    private Vector3 velocity;

    void Start()
    {
        InvokeRepeating("flipDir", turnInterval + initialDelay, turnInterval);
    }
    public Vector3 GetVelocity()
    {
        return velocity;
    }
    
    private void flipDir()
    {
        direction *= -1;
    }
    private void FixedUpdate()
    {
        velocity = new Vector3(speed * startDirection.x * direction, speed * startDirection.y * direction, 0);
        Vector3 curPos = transform.position;
        Vector3 newPos = curPos + velocity;
        transform.position = newPos;
    }
}
