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
    private PlayerShoot basicShootScript;
    private PlayerShoot powerShootScript;
    private GameObject shield;

    private bool m_Grounded;
    private bool m_FacingRight = true;
    private bool isStunned = false;
    private bool isAttacking = false;
    private bool isJumpingUp = false;
    private const float maxJumpTime = 13.0f;
    private float curJumpTime = 0.0f;
    private bool canJumpAgain = true;
    private float energy;
    private const float maxEnergy = 100.0f;
    private const float powerShotCost = 10.0f;
    public FloatReference speed;
    public FloatVariable speed2;
    // private bool shooting = false;

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
        m_HealthScript.onDeathDelegate = onDeath;
        m_Flash = GetComponent<Flash>();
        basicShootScript = GameObject.Find("BasicWeapon").GetComponent<PlayerShoot>();
        powerShootScript = GameObject.Find("PowerWeapon").GetComponent<PlayerShoot>();
        energy = maxEnergy;
        shield = GameObject.Find("PlayerShield");
        shield.SetActive(false);

        m_Grounded = false;
    }

    private void onHurt()
    {
        m_Flash.SetIsFlashing(true);
       // isStunned = true;
    }
    private void onDeath()
    {
        this.gameObject.SetActive(false);
        this.enabled = false;
        this.GetComponent<SpriteRenderer>().enabled = false;
        FindObjectOfType<GameController>().OnPlayerDeath();
    }
    private void onInvulnEnd()
    {
        m_Flash.SetIsFlashing(false);
        isStunned = false;
    }

    public float getEnergy()
    {
        return energy;
    }

    public void giveEnergy(float add)
    {
        energy += add;
        energy = Mathf.Clamp(energy, 0, maxEnergy);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void shootPressed()
    {
        if (basicShootScript.TryShoot(m_FacingRight))
        {
            float cooldownTime = 0.3f;
            AnimatorStateInfo animationState = m_Anim.GetCurrentAnimatorStateInfo(0);
            m_Anim.SetFloat("RunAnimPos", animationState.normalizedTime);
            m_Anim.SetLayerWeight(0, 0.0f);
            m_Anim.SetLayerWeight(1, 1.0f);
            m_Anim.SetBool("IsShooting", true);
            Invoke("shootEnd", cooldownTime);
        }
    }
    public void powerShotPressed()
    {
        if(energy >= powerShotCost)
        {
            energy -= powerShotCost;
        }
        else
        {
            return;
        }
        if (powerShootScript.TryShoot(m_FacingRight))
        {
            float cooldownTime = 0.3f;
            AnimatorStateInfo animationState = m_Anim.GetCurrentAnimatorStateInfo(0);
            m_Anim.SetFloat("RunAnimPos", animationState.normalizedTime);
            m_Anim.SetLayerWeight(0, 0.0f);
            m_Anim.SetLayerWeight(1, 1.0f);
            m_Anim.SetBool("IsShooting", true);
            Invoke("shootEnd", cooldownTime);
        }
    }

    private bool shielding;
    private void tryShieldOn()
    {
        if(energy > 0)
        {
            energy -= 0.10f;
            shieldOn();
        }
        else
        {
            shieldOff();
        }
    }
    private void shieldOn()
    {
        m_HealthScript.CannotHurt = true;
        shielding = true;
        shield.SetActive(true);
    }
    private void shieldOff()
    {
        m_HealthScript.CannotHurt = false;
        shielding = false;
        shield.SetActive(false);
    }

    private void shootEnd()
    {
        AnimatorStateInfo animationState = m_Anim.GetCurrentAnimatorStateInfo(0);
        m_Anim.SetFloat("RunAnimPos", animationState.normalizedTime);
     //   Debug.Log("ENDnormtime" + animationState.normalizedTime);
        m_Anim.SetLayerWeight(0, 1.0f);
        m_Anim.SetLayerWeight(1, 0.0f);
        m_Anim.SetBool("IsShooting",false);
    }
    //should be called by PlayerInput.FixedUpdate
    public void Move(float xMoveDir, bool crouch, bool jumpPressed, bool running)
    {
        speed2.Value += 1;
        crouch = false;

        if (xMoveDir > 0 && !m_FacingRight)
        {
            Flip();
        }
        else if (xMoveDir < 0 && m_FacingRight)
        {
            Flip();
        }

        float speed = xMoveDir * (shielding ? m_XSpeedBoost : m_XSpeed);
        bool hitTileX = false, hitTileY = false;
        if (!isStunned)
        {
            m_BasicMovement.setVelocityX(speed);

        }
        else
        {
            m_BasicMovement.setVelocityX(0);

        }
        if (running)
        {
            tryShieldOn();
        }
        else
        {
            shieldOff();
        }

        bool wasOnGround = m_Grounded && !jumpPressed;
        m_BasicMovement.Move(ref hitTileX, ref hitTileY, wasOnGround);

        m_Grounded = m_BasicMovement.OnGround();

        m_Anim.SetBool("OnGround", m_Grounded);
        m_Anim.SetFloat("XSpeed", Mathf.Abs(speed) * 10);
        float animYSpeed = Mathf.Abs(m_BasicMovement.GetVelocity().y);
        if (m_Grounded)
        {
            animYSpeed = 0;
        }
        m_Anim.SetFloat("YSpeed", animYSpeed);
       // m_Anim.SetFloat("RunAnimSpeed", Mathf.Abs(speed) * 10);


        if(m_Grounded && !jumpPressed)
        {
            canJumpAgain = true;
        }
        if (m_Grounded && jumpPressed && !isJumpingUp && canJumpAgain)
        {
            isJumpingUp = true;
            curJumpTime = 0;
            canJumpAgain = false;
        }
        if (isJumpingUp)
        {
            m_BasicMovement.setVelocityY(m_JumpVelocity);
            curJumpTime += 1.0f;
            if(curJumpTime > maxJumpTime)
            {
                isJumpingUp = false;
            }
        }
        if(isJumpingUp && !jumpPressed)
        {
            isJumpingUp = false;
        }
        if (!isJumpingUp && m_BasicMovement.GetVelocity().y > 0)
        {
            m_BasicMovement.setVelocityY(0);
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
