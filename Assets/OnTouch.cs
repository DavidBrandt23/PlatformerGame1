using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnTouch : MonoBehaviour {
    public bool destroySelf;
    public bool tryDamageOther;
    public bool tryHealOther;
    public int damageAmount;
    public int healAmount;
    public bool addBingo;
    public List<string> otherTags;
    public AudioClip audioClip;
    [SerializeField] public UnityEvent myEvent;
    // Use this for initialization
    void Start () {

        Collideable c = GetComponent<Collideable>();
        if (tryDamageOther)
        {
            c.onCollideDeleage += TryDamageOther;
        }
        if (tryHealOther)
        {
            c.onCollideDeleage += TryHealOther;
        }
        if (audioClip != null)
        {
            c.onCollideDeleage += PlaySound;
        }
        if (myEvent != null)
        {
            c.onCollideDeleage += CallEvent;
        }
        if (destroySelf)
        {
            c.onCollideDeleage += DestroySelf;
        }
        if (addBingo)
        {
            c.onCollideDeleage += AddBingo;
        }

    }

    private bool OtherHasCorrectTag(GameObject other)
    { 
        return (otherTags.Contains(other.tag));
    }

    private void AddBingo(GameObject other)
    {
        if (OtherHasCorrectTag(other))
        {
            MyGlobal.GetGameControllerObject().GetComponent<GameController>().AddBingoCard();
        }
    }
    private void DestroySelf(GameObject other)
    {
        if (OtherHasCorrectTag(other))
        {
            Destroy(gameObject);
        }
    }
    private void PlaySound(GameObject other)
    {
        if (OtherHasCorrectTag(other))
        {
            MyGlobal.PlayGlobalSound(audioClip);
        }
    }


    private void TryDamageOther(GameObject other)
    {
        if (OtherHasCorrectTag(other))
        {
            HealthScript hs = other.GetComponent<HealthScript>();
            if (hs != null)
            {
                hs.Damage(damageAmount);
            }
        }
    }
    private void TryHealOther(GameObject other)
    {
        if (OtherHasCorrectTag(other))
        {
            HealthScript hs = other.GetComponent<HealthScript>();
            if (hs != null)
            {
                hs.Heal(healAmount);
            }
        }
    }
    private void CallEvent(GameObject other)
    {
        if (OtherHasCorrectTag(other))
        {
            myEvent.Invoke();
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
