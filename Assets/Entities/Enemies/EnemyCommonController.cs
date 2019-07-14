using UnityEngine;
using System.Collections;

public class EnemyCommonController : EntityCommonController
{

    private HealthScript m_HealthScript;
    protected override void Start()
    {
        base.Start();

        m_HealthScript = GetComponent<HealthScript>();
        m_HealthScript.onHurtDelegate += onHurt;
        m_HealthScript.onInvulnEndDelegate += onInvulnEnd;

        m_Collideable.onCollideDeleage += onCollide;
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
        if (IsInvoking(stopFlashFunc))
        {
            CancelInvoke(stopFlashFunc);
        }
        Invoke(stopFlashFunc, 0.3f);
    }

    private const string stopFlashFunc = "stopFlash";
    private void stopFlash()
    {
        m_Flash.SetIsFlashing(false);
    }


    private void onInvulnEnd()
    {
       // m_Flash.SetIsFlashing(false);
    }
}
