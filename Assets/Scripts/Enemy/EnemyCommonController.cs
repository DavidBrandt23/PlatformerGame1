using UnityEngine;
using System.Collections;

public class EnemyCommonController : MonoBehaviour
{
    protected BoxCollider2D m_BoxCollider;
    protected BasicMovement m_BasicMovement;
    private HealthScript m_HealthScript;
    private Flash m_Flash;
    
    protected virtual void Start()
    {
        m_BoxCollider = GetComponent<BoxCollider2D>();
        m_BasicMovement = this.GetComponent<BasicMovement>();
        m_HealthScript = GetComponent<HealthScript>();
        m_HealthScript.onHurtDelegate += onHurt;
        m_HealthScript.onInvulnEndDelegate += onInvulnEnd;
        m_Flash = GetComponent<Flash>();

        Collideable c = GetComponent<Collideable>();
        c.onCollideDeleage = onCollide;
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
}
