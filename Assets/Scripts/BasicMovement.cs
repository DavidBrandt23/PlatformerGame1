using UnityEngine;
using System.Collections;

public class BasicMovement : MonoBehaviour
{
    [SerializeField] private float YMaxSpeed; 
    [SerializeField] private float GravityAccel;
    [SerializeField] private Vector2 velocity;
    [SerializeField] private AudioClip thudNoise;

    public Vector2 GetVelocity()
    {
        return velocity;
    }
    private Transform m_Transform;
    private BoxCollider2D m_BoxCollider;

    private void Start()
    {
        m_BoxCollider = GetComponent<BoxCollider2D>();
        m_Transform = GetComponent<Transform>();
    }
    public void setVelocityX(float x)
    {
        velocity = new Vector2(x, velocity.y);
    }

    public void setVelocityY(float y)
    {
        velocity = new Vector2(velocity.x, y);
    }

    public void setVelocity(float x, float y)
    {
        setVelocityX(y);
        setVelocityY(y);
    }

    //should be called by control scripts FixedUpdate
    public void Move(ref bool hitTileX, ref bool hitTileY)
    {
        velocity.y += GravityAccel;

        if (velocity.y < YMaxSpeed) velocity.y = YMaxSpeed;
        velocity = new Vector2(velocity.x, velocity.y);
        m_Transform.position = MyGlobal.GetValidPosition(m_Transform.position, m_BoxCollider, velocity, ref hitTileX, ref hitTileY);
    }
}
