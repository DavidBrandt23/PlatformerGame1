using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberController : EnemyCommonController
{
    public GameObject bulletPrefab;
    Vector3 bulletSourcePos;

    private int xDir;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        Shoot();
        xDir = -1;
        InvokeRepeating("changeDir", 2.0f, 2.0f);
    }
    private void Awake()
    {
        
    }
    private void changeDir()
    {

        xDir *= -1;
    }

    private void Shoot()
    {
        Vector3 direction = new Vector3(0, -1, 0);
        //bool right = m_BasicMovement.GetVelocity().x > 0;
        //if (right)
        //{
        //    direction = new Vector3(1, 0, 0);
        //}
        bulletSourcePos = transform.Find("BulletSource").transform.position;
        GameObject newBullet = MyGlobal.AddEntityToScene(bulletPrefab, bulletSourcePos);
        newBullet.GetComponent<BulletMove>().direction = direction;
        // m_AudioSource.PlayOneShot(shootNoise);
        Invoke("Shoot", 3.0f);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        bool hitTileX = false, hitTileY = false;
        m_BasicMovement.setVelocityX(xDir * 0.1f);
        m_BasicMovement.Move(ref hitTileX, ref hitTileY);

        //flip sprite to face move direction
        Vector3 theScale = transform.localScale;
        theScale.x = xDir;
        transform.localScale = theScale;
    }
}
