using UnityEngine;
using System.Collections;

public class Robot1Controller : EnemyCommonController
{
    [SerializeField] private float moveSpeed = 0.05f;
    [SerializeField] private float xDir = -1;

    public GameObject bulletPrefab;
    public bool shoots;

    protected override void Start()
    {
        base.Start();
        if (shoots)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        Vector3 bulletPos = new Vector3(transform.position.x, transform.position.y + 0.4f, transform.position.z);

        Vector3 direction = new Vector3(-1, 0, 0);
        bool right = m_BasicMovement.GetVelocity().x > 0;
        if (right)
        {
            direction = new Vector3(1, 0, 0);
        }
        GameObject newBullet = Instantiate(bulletPrefab, bulletPos, Quaternion.identity);
        newBullet.GetComponent<BulletMove>().direction = direction;
        newBullet.transform.parent = GameObject.Find("Entities").transform;
       // m_AudioSource.PlayOneShot(shootNoise);
        Invoke("Shoot", 3.0f);
    }

    private void onCollide(GameObject other)
    {
        if (other.tag.Equals("Player"))
        {
            other.GetComponent<HealthScript>().Damage(1);
        }
    }
    
    private void FixedUpdate()
    {
        Animator m_Anim = GetComponent<Animator>();
        AnimatorStateInfo animationState = m_Anim.GetCurrentAnimatorStateInfo(0);
        float pos = animationState.normalizedTime;
        pos = pos - (int)pos;
        float curSpeed = moveSpeed;
        Debug.Log("pos=" + pos);
        if (pos > 0.4f && pos < 0.8f)
        {
            curSpeed = 0;
        }
        float xVelocity = xDir * curSpeed;
        m_BasicMovement.setVelocityX(xVelocity);

        bool hitTileX = false, hitTileY = false;
        m_BasicMovement.Move(ref hitTileX, ref hitTileY);
        
        if (hitTileX)
        {
            xDir = xDir * -1;
        }

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x = xDir;
        transform.localScale = theScale;
    }
}
