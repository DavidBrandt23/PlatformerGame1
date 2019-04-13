using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour {
    [SerializeField] private int MaxHP;
    [SerializeField] private float InvulnTime;
    [SerializeField] private AudioClip hurtNoise;
    [SerializeField] private AudioClip deathNoise;
    private AudioSource m_AudioSource;
    public GameObject explosionPrefab;


    private bool invuln;
    private float curInvulnTime;
    private bool dead;

    private bool cannotHurt;
    public bool CannotHurt
    {
        get
        {
            return cannotHurt;
        }
        set { cannotHurt = value; }
    }

    private int _currentHP;
    public int CurrentHP
    {
        get
        {
            return _currentHP;
        }
        private set
        {
            _currentHP = value;
        }
    }

    public string HealthString()
    {
        return CurrentHP + " / " + MaxHP;
    }

    public delegate void HurtDelegate();
    public HurtDelegate onHurtDelegate;
    public delegate void DeathDelegate();
    public HurtDelegate onDeathDelegate;

    public delegate void InvulnEndDelegate();
    public InvulnEndDelegate onInvulnEndDelegate;
    public void Heal(int amount)
    {
        CurrentHP += amount;
        if(CurrentHP > MaxHP)
        {
            CurrentHP = MaxHP;
        }
    }
    public void Damage(int amount)
    {
        if (invuln || CannotHurt)
        {
            return;
        }

        CurrentHP -= amount;
        m_AudioSource.PlayOneShot(hurtNoise);
        this.GetComponent<Flash>().FlashOnce();
        if (CurrentHP <= 0)
        {
            if(!dead)
            {
                dead = true;
                if (onDeathDelegate == null)
                {
                    Instantiate(explosionPrefab, GetComponent<Transform>().position, Quaternion.identity);
                    Destroy(this.gameObject);
                }
                else
                {
                    Instantiate(explosionPrefab, GetComponent<Transform>().position, Quaternion.identity);
                    onDeathDelegate();
                }
            }
           // m_AudioSource.PlayOneShot(deathNoise);
            ///this.GetComponent<DestroyOnTimer>().StartTimer();
        }
        else
        {
            invuln = true;
            curInvulnTime = InvulnTime;
            onHurtDelegate();
        }
    }

    private void FixedUpdate()
    {
        if (invuln)
        {
            curInvulnTime -= Time.fixedDeltaTime;
        }
        if(curInvulnTime <= 0)
        {
            invuln = false;
            if (onInvulnEndDelegate != null)
            {
                onInvulnEndDelegate();
            }
        }
    }

    // Use this for initialization
    void Start () {
        CurrentHP = MaxHP;
        m_AudioSource = this.GetComponent<AudioSource>();
        dead = false;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
