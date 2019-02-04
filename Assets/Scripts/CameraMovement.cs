using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    public float leftXBound;
    public float rightXBound;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate() {
        GameObject playerOb = GameObject.Find("PlayerMan");
        if(playerOb == null)
        {
            return;
        }
        Transform transform = GetComponent<Transform>();
        float newX = playerOb.GetComponent<Transform>().position.x;
        newX = Mathf.Clamp(newX, leftXBound, rightXBound);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
	}
}
