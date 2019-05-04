using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    public float leftXBound;
    public float rightXBound;
    private const float cameraWidth = 30.0f;
    private const float cameraHeight = 17.0f;
    private const float xBuffer = 15.0f;
    private const float yBuffer = 5.0f;
    public Vector3Variable FollowObjectPos;
    public bool shouldFollowTarget = false;
    // Use this for initialization
    void Awake () {

        // Switch to 640 x 480 full-screen at 60 hz
        //  Screen.SetResolution(480, 270, false, 60);
        if(FollowObjectPos != null && shouldFollowTarget)
        {
            transform.position = FollowObjectPos.Value;
        }

        Vector3 curPos = transform.position;
        transform.position = new Vector3(curPos.x, curPos.y, -1);
    }
	
	// Update is called once per frame
	void FixedUpdate() {
        if (FollowObjectPos == null || !shouldFollowTarget)
        {
            return;
        }

        Transform transform = GetComponent<Transform>();
        
        Vector3 playerPos = FollowObjectPos.Value;
        float newX = playerPos.x;
        float newY = playerPos.y;
        //float newX = transform.position.x;
        //float newY = transform.position.y;

        //float halfWidth = cameraWidth / 2.0f, halfHeight = cameraHeight / 2.0f;

        //float cameraLeftX = newX - halfWidth;
        //float cameraRightX = newX + halfWidth;
        //float cameraTopY = newY + halfHeight;
        //float cameraBottomY = newY - halfHeight;

        //if(distance(cameraLeftX, playerPos.x) < xBuffer)
        //{
        //    newX = playerPos.x + (halfWidth - xBuffer);
        //}
        //else if (distance(cameraRightX, playerPos.x) < xBuffer)
        //{
        //    newX = playerPos.x - (halfWidth - xBuffer);
        //}

        //if (distance(cameraTopY, playerPos.y) < yBuffer)
        //{
        //    newY = playerPos.y - (halfHeight - yBuffer);
        //}
        //else if (distance(cameraBottomY, playerPos.y) < yBuffer)
        //{
        //    newY = playerPos.y + (halfHeight - yBuffer);
        //}



        //newX = Mathf.Clamp(newX, leftXBound, rightXBound);

        transform.position = new Vector3(newX, newY, transform.position.z);






        // float newY = transform.position.y;
        // newX = newX - (float) extra;
        // double extra = newX % 0.0625;
        // transform.position = new Vector3(newX, transform.position.y, transform.position.z);

        //int PPU = 16;
        //Vector3 position = transform.position;

        //position.x = (Mathf.Round(position.x * PPU) / PPU);
        //position.y = (Mathf.Round(position.y * PPU) / PPU);

        //transform.position = position;
    }
    private float distance(float a,float b)
    {
        return Mathf.Abs(a - b);
    }
}
