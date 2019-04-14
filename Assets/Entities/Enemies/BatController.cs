using UnityEngine;
using System.Collections;

public class BatController : EnemyCommonController
{

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        float speed = 0.04f;
        GameObject player = MyGlobal.GetPlayerObject();
        Vector3 currentMoveTarget = player.GetComponent<Transform>().position;
        Vector2 currentPos = transform.position;
        Vector2 lineToTarget = (Vector2)((Vector2)currentMoveTarget - currentPos);
        Vector2 direction = (lineToTarget).normalized;
        float distanceToTarget = lineToTarget.magnitude;

        if (distanceToTarget <= speed)
        {
            speed = distanceToTarget;
        }
        Vector2 newVelocity = (direction * speed);
        m_BasicMovement.setVelocityX(newVelocity.x);
        m_BasicMovement.setVelocityY(newVelocity.y);

        bool hitTileX = false, hitTileY = false;
        m_BasicMovement.Move(ref hitTileX, ref hitTileY);
    }
}
