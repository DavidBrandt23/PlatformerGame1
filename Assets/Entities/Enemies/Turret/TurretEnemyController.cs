using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEnemyController : EnemyCommonController
{
    Vector3 bulletSourcePos;
    public GameObject bulletPrefab;
    public ScriptableObject a;
    public int facingDir;
    public AudioClip shootNoise;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        bulletSourcePos = transform.Find("BulletSource").transform.position;
        Shoot();
    }

    private void Shoot()
    {
        Vector3 direction = new Vector3(facingDir, 0, 0);
        
        GameObject newBullet = MyGlobal.AddEntityToScene(bulletPrefab, bulletSourcePos);
        newBullet.GetComponent<BulletMove>().direction = direction;

        MyGlobal.PlayGlobalSoundIfOnScreen(shootNoise,1.0f,gameObject);
        Invoke("Shoot", 3.0f);
    }

    void FixedUpdate()
    {

        //flip sprite to face facingDir direction
        Vector3 theScale = transform.localScale;
        theScale.x = -1 * facingDir;
        transform.localScale = theScale;
    }
}
