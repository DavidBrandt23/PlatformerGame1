using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Vector3Reference targetPosition;
    private float initialXOffset;
    private Vector3 initialPos;
    private Vector3 initialPlayerPos;

    // Start is called before the first frame update
    void Start()
    {
        //initialXOffset = transform.position.x - targetPosition.Value.x;
        initialPos = transform.position;
        setInitialOffset();
    }

    public void setInitialOffset()
    {
        //  initialPlayerPos = targetPosition.Value;
        initialPlayerPos = new Vector3(28.5f, 18.5f, -1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        float speed = 0.5f; //0.7
        float newX = (initialPlayerPos.x - targetPosition.Value.x)*-speed + initialPos.x;

        Vector3 newPos = transform.position;
        newPos = new Vector3(newX, initialPos.y, initialPos.z);
        transform.position = newPos;
    }
}
