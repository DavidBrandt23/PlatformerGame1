using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEnemyController : EnemyCommonController
{
    Vector3 bulletSourcePos;
    public GameObject bulletPrefab;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        bulletSourcePos = transform.Find("BulletSource").transform.position;
        Shoot();
    }

    private void Shoot()
    {
        Vector3 direction = new Vector3(-1, 0, 0);
        //bool right = m_BasicMovement.GetVelocity().x > 0;
        //if (right)
        //{
        //    direction = new Vector3(1, 0, 0);
        //}
        GameObject newBullet = MyGlobal.AddEntityToScene(bulletPrefab, bulletSourcePos);
        newBullet.GetComponent<BulletMove>().direction = direction;
        // m_AudioSource.PlayOneShot(shootNoise);
        Invoke("Shoot", 3.0f);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
