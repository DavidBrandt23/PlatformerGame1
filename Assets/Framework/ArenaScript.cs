using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ArenaScript : MonoBehaviour
{
    public List<GameObject> EnemySets;
    public GameObject triggerRegion;
    public int currentSetNum = 0;
    public bool enemiesIn;
    // Use this for initialization
    void Start()
    {

    }
    private void OnEnable()
    {
        InvokeRepeating("spawnNextIfEmpty", 1.0f, 1.0f);
    }
    private bool enemiesInRegion()
    {
        List<GameObject> touchingObs = MyGlobal.GetTouchingObjects(triggerRegion.GetComponent<BoxCollider2D>());
        foreach(GameObject obj in touchingObs)
        {
            if (obj.tag.Contains("Enemy"))
            {
                return true;
            }
        }
        return false;
    }

    private const string spawnFunc = "spawnNextSet";
    private void spawnNextSet()
    {
        if (currentSetNum >= EnemySets.Count)
        {
            return;
        }

        EnemySets[currentSetNum].SetActive(true);
        currentSetNum++;
    }
    private void spawnNextIfEmpty()
    {
        if (!enemiesInRegion())
        {
            spawnNextSet();
        }
    }

    private void FixedUpdate()
    {
    }
    // Update is called once per frame
    void Update()
    {
        enemiesIn = enemiesInRegion();
    }
}
  