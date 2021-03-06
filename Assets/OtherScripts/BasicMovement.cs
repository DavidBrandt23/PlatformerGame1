﻿using UnityEngine;
using System.Collections;

public class BasicMovement : MonoBehaviour
{
    [SerializeField] private float YMaxSpeed;
    [SerializeField] private bool ignoreGravity;
    private const float GravityAccel = -0.015f;
    private const float MaxFallSpeed = -0.8f;
    [SerializeField] private AudioClip thudNoise;
    private DetectMovingPlat detectMovingPlatScript;
    
    private Transform m_Transform;
    private BoxCollider2D m_BoxCollider;

    [SerializeField] private Vector2 velocity;
    public Vector2 GetVelocity()
    {
        return velocity;
    }


    public bool OnGround()
    {
        return MyGlobal.OnGround(m_Transform.position, m_BoxCollider);
    }

    private void Start()
    {
        m_BoxCollider = GetComponent<BoxCollider2D>();
        m_Transform = GetComponent<Transform>();
        detectMovingPlatScript = GetComponent<DetectMovingPlat>();
        if(detectMovingPlatScript == null)
        {
            gameObject.AddComponent<DetectMovingPlat>();
        }
    }
    public void setVelocityX(float x)
    {
        velocity = new Vector2(x, velocity.y);
    }

    public void setVelocityY(float y)
    {
        velocity = new Vector2(velocity.x, y);
        if (velocity.y < MaxFallSpeed) velocity.y = MaxFallSpeed;
    }

    public void setVelocity(float x, float y)
    {
        setVelocityX(x);
        setVelocityY(y);
    }

    public void Move()
    {
        bool hitTileX = false, hitTileY = false;
        Move(ref hitTileX, ref hitTileY);
    }
    //should be called by control scripts FixedUpdate
    //ignoreGroundHandling will make Move not do the special handling it normally does to make entities "stick" to slope
    public void Move(ref bool hitTileX, ref bool hitTileY, bool wasOnGround = false)
    {
        if (!ignoreGravity)
        {
            setVelocityY(velocity.y + GravityAccel);
        }

        Vector2 moveVel = velocity;
        if (wasOnGround)
        {
           // moveVel.y -= 0.3f;
        }
        if(detectMovingPlatScript != null)
        {
            GameObject movePlat = detectMovingPlatScript.getTouchedPlat();
            if(movePlat != null)
            {
                MovingPlatform movePlatScript = movePlat.GetComponent<MovingPlatform>();
                if(movePlatScript != null)
                {
                    m_Transform.position += movePlatScript.GetVelocity();
                }
            }
        }
        float oldZ = m_Transform.position.z;
        m_Transform.position = MyGlobal.GetValidPosition(m_Transform.position, m_BoxCollider, moveVel, ref hitTileX, ref hitTileY, wasOnGround);
        m_Transform.position = new Vector3(m_Transform.position.x, m_Transform.position.y, oldZ);
        bool onGround = OnGround();

        if (!wasOnGround && onGround && velocity.y < -0.05f)
        {
          //  MyGlobal.PlayGlobalSound(thudNoise);// re enable once I get volume control
        }
        if (onGround)
        {
            setVelocityY(0);
        }
    }
}
