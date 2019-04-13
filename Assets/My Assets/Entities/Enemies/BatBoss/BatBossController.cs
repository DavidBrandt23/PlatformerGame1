using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatBossController : EnemyCommonController
{

    private void FixedUpdate()
    {
        Vector3 endPoint = GameObject.Find("end").transform.position;
        Vector3 curPoint = transform.position;
        Vector3 dirToEnd = (endPoint - curPoint).normalized;
        float speed = 0.1f;
        Vector3 velocity = dirToEnd * speed;
        BasicMovement bm = GetComponent<BasicMovement>();
        bm.setVelocity(velocity.x, velocity.y);
        bool hitX = false, hitY = false;
        bm.Move(ref hitX, ref hitY);
    }
}
