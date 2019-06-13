using UnityEngine;
using System.Collections;

public class NonGridBlock : MonoBehaviour
{
    public RuntimeSet_GameObject blockList;

    private void OnEnable()
    {
        blockList.Add(gameObject);
    }
    private void OnDisable()
    {
        blockList.Remove(gameObject);
    }

    // Use this for initialization
    void Start()
    {

    }
    
}
