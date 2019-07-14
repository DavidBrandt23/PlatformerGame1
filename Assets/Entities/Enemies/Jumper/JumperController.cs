using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperController : EnemyCommonController
{
    public float JumpYSpeed;
    public float JumpXSpeed;
    public float JumpIntervalSeconds;
    public Vector3Variable targetPosition;

    private int xDir = -1;

    protected override void Start()
    {
        base.Start();
        InvokeRepeating("jumpAnim", 0.0f, JumpIntervalSeconds);
        InvokeRepeating("jump", 0.5f, JumpIntervalSeconds);
    }

    private void jumpAnim()
    {
        GetComponent<Animator>().SetTrigger("Jump");
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
        xDir = moveRight ? 1 : -1;
        m_BasicMovement.setVelocityX(JumpXSpeed * xDir);
        m_BasicMovement.setVelocityY(JumpYSpeed);
    }
    private void FixedUpdate()
    {

        bool hitTileX = false, hitTileY = false;
        m_BasicMovement.Move(ref hitTileX, ref hitTileY);
        if (hitTileX || hitTileY)
        {

            m_BasicMovement.setVelocityX(0.0f);
        }
        faceDirection();
    }

    private void faceDirection()
    {

        //flip sprite to face move direction
        Vector3 theScale = transform.localScale;
        theScale.x = xDir * -1;
        transform.localScale = theScale;
    }
}
