using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperController : EnemyCommonController
{
    public float JumpYSpeed;
    public float JumpXSpeed;
    public float JumpIntervalSeconds;
    public Vector3Variable targetPosition;
    protected override void Start()
    {
        base.Start();
        InvokeRepeating("jump", 0.0f, JumpIntervalSeconds);
    }

    private void jump()
    {
        if(targetPosition == null)
        {
            return;
        }
        float disToTarget = (targetPosition.Value - transform.position).magnitude;
        if(disToTarget > 20)
        {
            return;
        }

        bool moveRight = (targetPosition.Value.x - transform.position.x) > 1;
        float xDir = moveRight ? 1.0f : -1.0f;
        m_BasicMovement.setVelocityX(JumpXSpeed * xDir);
        m_BasicMovement.setVelocityY(JumpYSpeed);
    }
    private void FixedUpdate()
    {

        bool hitTileX = false, hitTileY = false;
        m_BasicMovement.Move(ref hitTileX, ref hitTileY);
        if(hitTileX || hitTileY)
        {

            m_BasicMovement.setVelocityX(0.0f);
        }
    }
}
