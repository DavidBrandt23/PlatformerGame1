using UnityEngine;
using System.Collections;

public class PlayerShoot : MonoBehaviour
{

    [SerializeField] private AudioClip shootNoise;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float cooldownTime;
    private bool offCooldown;

    private AudioSource m_AudioSource;

    void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
        offCooldown = true;
    }
    
    protected void endCooldown()
    {
        offCooldown = true;
    }
    protected virtual bool CanShoot()
    {
        return offCooldown;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="right">Will shoot left if false</param>
    public bool TryShoot(bool right)
    {
        if (!CanShoot())
        {
            return false;
        }
        float bulletStartY = transform.position.y + 0.35f;
        float bulletStartX = transform.position.x;
        float xOffset = 0.6f;
        if(!right)
        {
            xOffset *= -1;
        }
        bulletStartX += xOffset;
        Vector3 bulletPos = new Vector3(bulletStartX, bulletStartY, transform.position.z);

        Vector3 direction = new Vector3(-1, 0, 0);
        if (right)
        {
            direction = new Vector3(1, 0, 0);
        }
        GameObject newBullet = Instantiate(bulletPrefab, bulletPos, Quaternion.identity);
        newBullet.GetComponent<BulletMove>().direction = direction;
        newBullet.transform.parent = GameObject.Find("Entities").transform;

        MyGlobal.PlayGlobalSound(shootNoise);

        offCooldown = false;
        Invoke("endCooldown", cooldownTime);
        return true;
    }
}
