using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    // Start is called before the first frame update
    private float direction = 1;
    private Vector3 velocity;
    void Start()
    {
        InvokeRepeating("flipDir", 0.0f, 2.0f);
    }
    public Vector3 GetVelocity()
    {
        return velocity;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void flipDir()
    {
        direction *= -1;
    }
    private void FixedUpdate()
    {
        float speed = 0.05f;
        velocity = new Vector3(speed * direction,0, 0);
        Vector3 curPos = transform.position;
        Vector3 newPos = curPos + velocity;
        transform.position = newPos;
    }
}
