using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour {


    public float scrollSpeed;
    public float tileSizeX;
    public float cameraXOffset;
    public bool followCamera;
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        Transform transform = GetComponent<Transform>();
        if (followCamera)
        {

            GameObject playerOb = GameObject.Find("MainCamera");
            float newX = playerOb.GetComponent<Transform>().position.x + cameraXOffset;
            transform.position = new Vector3(newX, transform.position.y, transform.position.z);
        }
        else
        {

            GameObject playerOb = GameObject.Find("MainCamera");
            float cameraX = playerOb.GetComponent<Transform>().position.x;
            // transform.position = new Vector3(newX, transform.position.y, transform.position.z);
            // float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSizeX);
            float newPosition = Mathf.Repeat(cameraX * -scrollSpeed, tileSizeX) + cameraXOffset;
            transform.position = startPosition + (new Vector3(1, 0, 0)) * newPosition;
        }
    }
    // Update is called once per frame
    void FixedUpdate () {

       // GameObject playerOb = GameObject.Find("MainCamera");
       // Transform transform = GetComponent<Transform>();
       // float newX = playerOb.GetComponent<Transform>().position.x + xOffset;
       //// newX = Mathf.Clamp(newX, 12, 34);
       // transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }
}
