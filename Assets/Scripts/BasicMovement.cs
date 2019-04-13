using UnityEngine;
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

    //should be called by control scripts FixedUpdate
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
        m_Transform.position = MyGlobal.GetValidPosition(m_Transform.position, m_BoxCollider, moveVel, ref hitTileX, ref hitTileY, wasOnGround);
        bool onGround = OnGround();

        if (!wasOnGround && onGround && velocity.y < -0.05f)
        {
            GetComponent<AudioSource>().PlayOneShot(thudNoise, 0.05f);
        }
        if (onGround)
        {
            setVelocityY(0);
        }
    }
}
