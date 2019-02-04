using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float m_XSpeed;
    [SerializeField] private float m_XSpeedBoost;
    [SerializeField] private float m_JumpVelocity;

    private BoxCollider2D m_BoxCollider;
    private BasicMovement m_BasicMovement;
    private Animator m_Anim;
    private AudioSource m_AudioSource;
    private Transform m_Transform;
    private HealthScript m_HealthScript;
    private Flash m_Flash;

    private bool m_Grounded;
    private bool m_FacingRight = true;
    private bool isStunned = false;
    private bool isAttacking = false;

    //should maybe be Damagable or something

    // Use this for initialization
    void Start()
    {
        m_BasicMovement = GetComponent<BasicMovement>();
        m_Anim = GetComponent<Animator>();
        m_AudioSource = GetComponent<AudioSource>();
        m_Transform = GetComponent<Transform>();
        m_BoxCollider = GetComponent<BoxCollider2D>();
        m_HealthScript = GetComponent<HealthScript>();
        m_HealthScript.onHurtDelegate = onHurt;
        m_HealthScript.onInvulnEndDelegate = onInvulnEnd;
        m_Flash = GetComponent<Flash>();
        

        m_Grounded = false;
    }

    private void onHurt()
    {
        m_Flash.SetIsFlashing(true);
       // isStunned = true;
    }

    private void onInvulnEnd()
    {
        m_Flash.SetIsFlashing(false);
        isStunned = false;
    }
    
    // Update is called once per frame
    void Update()
    {

    }

    public void shootPressed()
    {
        PlayerShoot ShootScript = this.GetComponent<PlayerShoot>();
        if (ShootScript.TryShoot(m_FacingRight))
        {

            m_Anim.SetTrigger("Punch");
        }
    }


    //should be called by PlayerInput.FixedUpdate
    public void Move(float xMoveDir, bool crouch, bool jump, bool boosting)
    {
        crouch = false;

        if (xMoveDir > 0 && !m_FacingRight)
        {
            Flip();
        }
        else if (xMoveDir < 0 && m_FacingRight)
        {
            Flip();
        }

        float speed = xMoveDir * (boosting ? m_XSpeedBoost : m_XSpeed);
        bool hitTileX = false, hitTileY = false;
        if (!isStunned)
        {
            m_BasicMovement.setVelocityX(speed);

        }
        else
        {
            m_BasicMovement.setVelocityX(0);

        }
        m_BasicMovement.Move(ref hitTileX, ref hitTileY);

       // if (hitTileY && velocity.y < -0.2f)
       // {
           // m_AudioSource.PlayOneShot(thudNoise, 0.3f);
        //}

        m_Anim.SetBool("OnGround", m_Grounded);
        m_Anim.SetFloat("XSpeed", Mathf.Abs(speed));
        m_Anim.SetFloat("YVelocity", m_BasicMovement.GetVelocity().y);
        m_Anim.SetFloat("RunAnimSpeed", Mathf.Abs(speed) * 10);


        m_Grounded = MyGlobal.OnGround(m_Transform.position, m_BoxCollider);
        if (m_Grounded)
        {
            m_BasicMovement.setVelocityY(0);
        }
        if (m_Grounded && jump)
        {
            m_BasicMovement.setVelocityY(m_JumpVelocity);
        }
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
