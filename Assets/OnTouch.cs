using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTouch : MonoBehaviour {
    public bool destroySelf;
    public bool tryDamageOther;
    public int damageAmount;
    public List<string> otherTags;
	// Use this for initialization
	void Start () {

        Collideable c = GetComponent<Collideable>();
        if (destroySelf)
        {
            c.onCollideDeleage += DestroySelf;
        }
        if (destroySelf)
        {
            c.onCollideDeleage += TryDamageOther;
        }

    }

    private bool OtherHasCorrectTag(GameObject other)
    { 
        return (otherTags.Contains(other.tag));
    }

    private void DestroySelf(GameObject other)
    {
        if (OtherHasCorrectTag(other))
        {
            Destroy(gameObject);
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

    // Update is called once per frame
    void Update () {
		
	}
}
