using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    public CameraSettings camSettings;

    private const float cameraWidth = 30.0f;
    private const float cameraHeight = 17.0f;
    private const float xBuffer = 15.0f;
    private const float yBuffer = 5.0f;
    public float maxSpeed = 0.1f;

    public GameEvent StartEvent;
    public Vector3Variable PositionVariable;

    // Use this for initialization
    void Awake () {

        // Switch to 640 x 480 full-screen at 60 hz
        //  Screen.SetResolution(480, 270, false, 60);
        //if(FollowObjectPos != null)
       // {
            //transform.position = FollowObjectPos.Value;
        //}

    }
    private void Start()
    {
        Vector3 curPos = transform.position;
        transform.position = new Vector3(curPos.x, curPos.y, -1);
        updatePositionReference();
        StartEvent.Raise();
    }

    public void setCameraSettings(CameraSettings settings)
    {
        camSettings.assignSettings(settings);
    }

    // Update is called once per frame
    void FixedUpdate() {
        Vector3 followPos = new Vector3();
        Transform transform = GetComponent<Transform>();
        float halfWidth = cameraWidth / 2.0f, halfHeight = cameraHeight / 2.0f;
        float newX = transform.position.x;
        float newY = transform.position.y;
        float minX = camSettings.leftXBound.Value + halfWidth, maxX = camSettings.rightXBound.Value - halfWidth, minY = camSettings.bottomYBound.Value + halfHeight, maxY = camSettings.topYBound.Value - halfHeight;
        float xOffsetFromTarget, yOffsetFromTarget;
        float xOffsetFromMinX = newX - minX;
        float xOffsetFromMaxX = newX - maxX;
        float yOffsetFromMinY = newY - minY;
        float yOffsetFromMaxY = newY - maxY;

       // if (FollowObjectPos != null)
       // {
            followPos = camSettings.targetPos.Value;
            xOffsetFromTarget = newX - followPos.x;
            yOffsetFromTarget = newY - followPos.y;
        //   }
        switch (camSettings.CameraMode)
        {
            case 0:
                return;

            case 1://follow target x exactly but respect x bound, only move y to get to bottom bound, top y bound ignored
                newX = GetValueJumpToTargetInBounds(followPos.x, minX, maxX);

                //get back in bounds
                if (yOffsetFromMinY < 0)
                {
                    newY += Mathf.Min(maxSpeed, yOffsetFromMinY * -1);
                }               
                if (yOffsetFromMinY > 0)
                {
                    newY -= Mathf.Min(maxSpeed, yOffsetFromMinY);
                }


                //respect max y
                //if (yOffsetFromMaxY > 0)
                //{
                //    newY -= Mathf.Min(maxSpeed, yOffsetFromMaxY);
                //}
                break;

            case 2://follow target x exactly but respect x bound, follow target y up to max speed but respect y bound
                newX = GetValueJumpToTargetInBounds(followPos.x, minX, maxX);
                newY = GetValueFollowTargetInBounds(newY, yOffsetFromTarget, yOffsetFromMinY, yOffsetFromMaxY, maxSpeed);
                break;
            case 3: //move x and y at max speed to target
                newX = GetValueFollowTargetInBounds(newX, xOffsetFromTarget, xOffsetFromMinX, xOffsetFromMaxX, maxSpeed);
                newY = GetValueFollowTargetInBounds(newY, yOffsetFromTarget, yOffsetFromMinY, yOffsetFromMaxY,maxSpeed);
                break;
            case 4: //fixed on position
                newX = GetValueFollowTargetInBounds(newX, xOffsetFromTarget, xOffsetFromMinX, xOffsetFromMaxX,99999.0f);
                newY = GetValueFollowTargetInBounds(newY, yOffsetFromTarget, yOffsetFromMinY, yOffsetFromMaxY, 99999.0f);
                break;
        }


        //float newX = transform.position.x;
        //float newY = transform.position.y;


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







        // float newY = transform.position.y;
        // newX = newX - (float) extra;
        // double extra = newX % 0.0625;
        // transform.position = new Vector3(newX, transform.position.y, transform.position.z);

        //int PPU = 16;
        //Vector3 position = transform.position;

        //position.x = (Mathf.Round(position.x * PPU) / PPU);
        //position.y = (Mathf.Round(position.y * PPU) / PPU);

        //transform.position = position;


        transform.position = new Vector3(newX, newY, transform.position.z);
        updatePositionReference();
    }

    private float GetValueJumpToTargetInBounds(float target, float min, float max)
    {
        return Mathf.Clamp(target, min, max);
    }
    private float GetValueFollowTargetInBounds(float curCamValue, float yOffsetFromTarget, float yOffsetFromMinY, float yOffsetFromMaxY,float maxSpeedForMove)
    {
        if (yOffsetFromTarget < 0) //target above
        {
            curCamValue += Mathf.Min(maxSpeedForMove, yOffsetFromMaxY * -1, yOffsetFromTarget * -1);
        }
        if (yOffsetFromTarget > 0)
        {
            curCamValue -= Mathf.Min(maxSpeedForMove, yOffsetFromMinY, yOffsetFromTarget);
        }
        return curCamValue;
    }

    private void updatePositionReference()
    {
        PositionVariable.Value = transform.position;
    }

    private float distance(float a,float b)
    {
        return Mathf.Abs(a - b);
    }
}
