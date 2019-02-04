using UnityEngine;
using System.Collections;

public class PlayerShoot : MonoBehaviour
{

    [SerializeField] private AudioClip shootNoise;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float cooldownTime;
    private bool canShoot;

    private AudioSource m_AudioSource;

    void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
        canShoot = true;
    }
    
    private void CanShoot()
    {
        canShoot = true;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="right">Will shoot left if false</param>
    public bool TryShoot(bool right)
    {
        if (!canShoot)
        {
            return false;
        }
        Vector3 bulletPos = new Vector3(transform.position.x, transform.position.y + 0.4f, transform.position.z);

        Vector3 direction = new Vector3(-1, 0, 0);
        if (right)
        {
            direction = new Vector3(1, 0, 0);
        }
        GameObject newBullet = Instantiate(bulletPrefab, bulletPos, Quaternion.identity);
        newBullet.GetComponent<BulletMove>().direction = direction;
        newBullet.transform.parent = GameObject.Find("Entities").transform;
        m_AudioSource.PlayOneShot(shootNoise);

        canShoot = false;
        Invoke("CanShoot", cooldownTime);
        return true;
    }
}
