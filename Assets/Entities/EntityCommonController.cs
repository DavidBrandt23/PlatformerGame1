using UnityEngine;
using System.Collections;

public class EntityCommonController : MonoBehaviour
{
    protected BoxCollider2D m_BoxCollider;
    protected BasicMovement m_BasicMovement;
    protected Collideable m_Collideable;
    protected Flash m_Flash;

    private DetectMovingPlat m_DetectMovingPlat;

    // Use this for initialization
    protected virtual void Start()
    {
        m_BoxCollider = GetComponent<BoxCollider2D>();
        m_BasicMovement = this.GetComponent<BasicMovement>();

        m_Flash = GetComponent<Flash>();
        if (m_Flash == null)
        {
            m_Flash = gameObject.AddComponent<Flash>();
        }

        m_Collideable = GetComponent<Collideable>();
        if (m_Collideable == null)
        {
            m_Collideable = gameObject.AddComponent<Collideable>();
        }

        m_DetectMovingPlat = GetComponent<DetectMovingPlat>();
        if (m_DetectMovingPlat == null)
        {
            m_DetectMovingPlat = gameObject.AddComponent<DetectMovingPlat>();
        }
    }
    //private void AddIfNotPresent<T>(T myProp as Component)
    //{
    //    myProp = GetComponent<T>();
    //    if (myProp == null)
    //    {
    //        myProp = gameObject.AddComponent<T>();
    //    }
    //}

}
