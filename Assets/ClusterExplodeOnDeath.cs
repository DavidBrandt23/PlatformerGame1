using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClusterExplodeOnDeath : MonoBehaviour
{
    public float xSize, ySize;
    public List<Behaviour> ThingsToDisable;
    public GameEvent gameEvent;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<HealthScript>().onDeathDelegate += onDeath;
    }

    private void onDeath()
    {
        if(gameEvent != null)
        {
            gameEvent.Raise();
        }
        
        foreach (Behaviour b in ThingsToDisable)
        {
            b.enabled = false;
        }
        MyGlobal.createExplosionCluster(transform, xSize, ySize);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
