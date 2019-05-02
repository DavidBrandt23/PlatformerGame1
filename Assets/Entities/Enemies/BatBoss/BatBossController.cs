using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatBossController : EnemyCommonController
{
    public List<GameObject> targets;
    private int listDir = 1;
    public int curTargetIndex = 0;
    

    private void FixedUpdate()
    {
        if(distanceToPoint(getCurTarget()) < 0.1f)
        {
            getNextTargetIndex();
        }

        MoveTowardPoint(getCurTarget());
    }
    private Vector3 getCurTarget()
    {
        return targets[curTargetIndex].transform.position;
    }

    private void getNextTargetIndex()
    {
        curTargetIndex += listDir;
        if(curTargetIndex > targets.Count - 1)
        {
            curTargetIndex -= 2;
            listDir = -1;
        }
        else if(curTargetIndex < 0){
            curTargetIndex = 1;
            listDir = 1;
        }
    }

    private void MoveTowardPoint(Vector3 endPoint)
    {
        Vector3 curPoint = transform.position;
        Vector3 dirToEnd = (endPoint - curPoint).normalized;
        float speed = 0.1f;
        Vector3 velocity = dirToEnd * speed;
        BasicMovement bm = GetComponent<BasicMovement>();
        bm.setVelocity(velocity.x, velocity.y);
        bool hitX = false, hitY = false;
        bm.Move(ref hitX, ref hitY);
    }
    private float distanceToPoint(Vector3 point)
    {
        Vector3 curPoint = transform.position;
        return (curPoint - point).magnitude;
    }
}
