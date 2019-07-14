using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserController : EnemyCommonController
{
    public float speed;
    public Vector3Variable targetPos;

    private float velocity;
    public int initialDirection = -1;

    private const float targetTurnAccel = 0.006f;

    //public float targetPosTurnDelay;
    //private bool turnInvoked;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        velocity = initialDirection * speed;

    }
    

    private void FixedUpdate()
    {

        float tX = targetPos.Value.x;
        float myX = transform.position.x;
        if ((tX < myX))
        {
            velocity -= targetTurnAccel;
        }
        else if ((tX > myX))
        {
            velocity += targetTurnAccel;
        }

        velocity = Mathf.Clamp(velocity, -1 * speed, speed);
        bool hitTileX = false, hitTileY = false;
        m_BasicMovement.setVelocityX(velocity);
        m_BasicMovement.Move(ref hitTileX, ref hitTileY, false);


        //flip sprite to face move direction
        Vector3 theScale = transform.localScale;
        int xMoveDir = (velocity < 0.0f)? -1 : 1;
        theScale.x = -1 * xMoveDir;
        transform.localScale = theScale;
    }
}
