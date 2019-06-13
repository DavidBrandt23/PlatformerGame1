using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerWhenClose : MonoBehaviour
{
    public Vector3Reference otherPos;
    public FloatReference triggerDistance;
    public List<Behaviour> scriptsToEnable;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        float distance = Mathf.Abs(transform.position.x - otherPos.Value.x);
        if(distance <= triggerDistance.Value)
        {
            foreach(Behaviour mb in scriptsToEnable)
            {
                mb.enabled = true;
            }
            Destroy(this);
        }
    }
}
