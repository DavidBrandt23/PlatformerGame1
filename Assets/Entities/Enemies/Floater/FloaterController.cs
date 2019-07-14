using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloaterController : EnemyCommonController
{
    public float speed;
    public Vector3Variable targetPos;

    private float velocity;
    private int initialDirection = -1;

    private const float targetTurnAccel = 0.006f;

    public bool shouldShoot;
    public GameObject bulletPrefab;
    public AudioClip shootNoise;


    //public float targetPosTurnDelay;
    //private bool turnInvoked;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        velocity = initialDirection * speed;
        if (shouldShoot)
        {
            Shoot();
        }

    }

    private void PrepareShoot()
    {
        //unused
        m_Flash.FlashOnce();
        Invoke("Shoot", 0.5f);
    }
    private void Shoot()
    {
        Vector3 direction = getLineToTarget().normalized;
        Vector3 bulletSourcePos = transform.position;
        GameObject newBullet = MyGlobal.AddEntityToScene(bulletPrefab, bulletSourcePos);
        newBullet.GetComponent<BulletMove>().direction = direction;

        MyGlobal.PlayGlobalSoundIfOnScreen(shootNoise, 1.0f, gameObject);
        Invoke("Shoot", 3.0f);
    }
    private Vector3 getLineToTarget()
    {

        Vector2 currentTarget = targetPos.Value;
        Vector2 currentPos = transform.position;
        Vector3 lineToTarget = (Vector2)((Vector2)currentTarget - currentPos);
        return lineToTarget;
    }
    private void FixedUpdate()
    {


        float speed = 0.04f;
        
        Vector2 lineToTarget = getLineToTarget();
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
        m_BasicMovement.Move(ref hitTileX, ref hitTileY, false);

        
    }
}
