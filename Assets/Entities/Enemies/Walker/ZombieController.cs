using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : EnemyCommonController
{
    public int xMoveDir = -1;
    public float speed;
    public bool turnAtEdge;
    public bool shouldShoot;
    public GameObject bulletPrefab;
    public AudioClip shootNoise;
    
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        if (shouldShoot)
        {
            Shoot();
        }

    }

    private void Shoot()
    {
        Vector3 direction = new Vector3(xMoveDir, 0, 0);
        Vector3 bulletSourcePos = transform.position;
        GameObject newBullet = MyGlobal.AddEntityToScene(bulletPrefab, bulletSourcePos);
        newBullet.GetComponent<BulletMove>().direction = direction;

        MyGlobal.PlayGlobalSoundIfOnScreen(shootNoise, 1.0f, gameObject);
        Invoke("Shoot", 3.0f);
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
