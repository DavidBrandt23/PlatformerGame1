using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    public float leftXBound;
    public float rightXBound;
    // Use this for initialization
    void Start () {

        // Switch to 640 x 480 full-screen at 60 hz
      //  Screen.SetResolution(480, 270, false, 60);
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
        double extra = newX % 0.0625;
       // newX = newX - (float) extra;
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);

        //int PPU = 16;
        //Vector3 position = transform.position;

        //position.x = (Mathf.Round(position.x * PPU) / PPU);
        //position.y = (Mathf.Round(position.y * PPU) / PPU);

        //transform.position = position;
    }
}
