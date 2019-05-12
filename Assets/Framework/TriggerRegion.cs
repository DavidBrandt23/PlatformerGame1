using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class TriggerRegion : MonoBehaviour
{
    public UnityEvent response;

    void OnEnable()
    {
        GetComponent<Collideable>().onCollideDeleage += callEvent;
    }
    void OnDisable()
    {
        GetComponent<Collideable>().onCollideDeleage -= callEvent;
    }

    private void callEvent(GameObject other)
    {
        if (other.tag == "Player")
        {
            response.Invoke();
        }
    }
    
}
