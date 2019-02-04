using UnityEngine;
using System.Collections;

public class Robot1Controller : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 0.05f;
    [SerializeField] private float xDir = -1;

    BoxCollider2D m_BoxCollider;
    private BasicMovement m_BasicMovement;
    private HealthScript m_HealthScript;
    private Flash m_Flash;

    public GameObject bulletPrefab;
    public bool shoots;

    void Start()
    {
        m_BoxCollider = GetComponent<BoxCollider2D>();
        m_BasicMovement = this.GetComponent<BasicMovement>();
        m_HealthScript = GetComponent<HealthScript>();
        m_HealthScript.onHurtDelegate = onHurt;
        m_HealthScript.onInvulnEndDelegate = onInvulnEnd;
        m_Flash = GetComponent<Flash>();

        Collideable c = GetComponent<Collideable>();
        c.onCollideDeleage = onCollide;
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

    private void onHurt()
    {
        m_Flash.SetIsFlashing(true);
    }
    private void onInvulnEnd()
    {
        m_Flash.SetIsFlashing(false);
    }

    private void FixedUpdate()
    {
        m_BasicMovement.setVelocityX(xDir * moveSpeed);

        bool hitTileX = false, hitTileY = false;
        m_BasicMovement.Move(ref hitTileX, ref hitTileY);
        
        if (hitTileX)
        {
            xDir = xDir * -1;
        }
    }
}
