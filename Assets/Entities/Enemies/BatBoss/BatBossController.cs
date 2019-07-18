using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatBossController : EnemyCommonController
{
    public List<GameObject> targets;
    public List<GameObject> sweepTargets;
    private int listDir = 1;
    public int curTargetIndex = 0;
    private float startSpeed = 0.1f;
    private float fastSpeed = 0.15f;

    private BatAction curBatAction;

    protected override void Start()
    {
        base.Start();
        curBatAction = BatAction.FollowPoints;
        m_HealthScript.onDeathDelegate += onDeath;
    }

    private void onDeath()
    {
        m_BoxCollider.enabled = false;
        MyGlobal.createExplosionCluster(transform, 2, 2);
    }

    private enum BatAction
    {
        FollowPoints,
        RandomPoints, 
        Sweep, 
    };

    private int randomCount = 0;
    private void FixedUpdate()
    {
        if (m_HealthScript.IsDead())
        {
            return;
        }
        if(curBatAction == BatAction.FollowPoints)
        {
            if (distanceToPoint(getCurTarget()) < 0.1f)
            {
                getNextTargetIndex();
            }
            if((curTargetIndex == 0) && (listDir == -1))
            {
                curBatAction = BatAction.RandomPoints;
                randomCount = 0;
            }
        }
        else if(curBatAction == BatAction.RandomPoints)
        {
            if (distanceToPoint(getCurTarget()) < 0.1f)
            {
                curTargetIndex = Random.Range(0, targets.Count);
                randomCount++;
            }
            if(randomCount >= 5)
            {
                curBatAction = BatAction.Sweep;
                curTargetIndex = 0;
            }
        }
        else if(curBatAction == BatAction.Sweep)
        {
            if (distanceToPoint(getCurTarget()) < 0.1f)
            {
                getNextTargetIndex();
            }
            if(curTargetIndex >= 2)
            {
                curBatAction = BatAction.FollowPoints;
                curTargetIndex = 0;
                listDir = 1;
            }
        }

        MoveTowardPoint(getCurTarget());
    }
    private Vector3 getCurTarget()
    {
        if(curBatAction == BatAction.Sweep)
        {
            return getCurSweepTarget();
        }
        return targets[curTargetIndex].transform.position;
    }
    private Vector3 getCurSweepTarget()
    {
        return sweepTargets[curTargetIndex].transform.position;
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
        bool closeToDeath = m_HealthScript.CurrentHP < 20;
        float speed = closeToDeath ? fastSpeed : startSpeed;
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
