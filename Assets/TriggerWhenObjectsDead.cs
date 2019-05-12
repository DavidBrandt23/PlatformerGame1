using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerWhenObjectsDead : MonoBehaviour
{
    public List<GameObject> objects;
    public UnityEvent myEvent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach(GameObject ob in objects)
        {
            if(ob != null)
            {
                return;
            }
        }
        myEvent.Invoke();
        Destroy(this.gameObject);
    }
}
