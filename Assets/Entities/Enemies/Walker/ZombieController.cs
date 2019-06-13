using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : EnemyCommonController
{
    private int xMoveDir = -1;
    public float speed;
    public bool turnAtEdge;
    
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

    }

    private void FlipXMoveDir()
    {
        xMoveDir *= -1;
    }

    private void FixedUpdate()
    {
        bool hitTileX = false, hitTileY = false;
        m_BasicMovement.setVelocityX(speed * xMoveDir);
        m_BasicMovement.Move(ref hitTileX, ref hitTileY, false);

            bool facingEdge = MyGlobal.facingEdge(transform.position, m_BoxCollider, xMoveDir);
            if ((turnAtEdge && facingEdge) || hitTileX)
            {
                FlipXMoveDir();
            }
        

        //flip sprite to face move direction
        Vector3 theScale = transform.localScale;
        theScale.x = -1 * xMoveDir;
        transform.localScale = theScale;
    }
}
