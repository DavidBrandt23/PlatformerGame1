using UnityEngine;
using System.Collections;

public class RespectGravity : MonoBehaviour
{

    // Use this for initialization
    void Awake()
    {
        if(GetComponent<BasicMovement>() == null)
        {
            gameObject.AddComponent<BasicMovement>();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        BasicMovement basicMovement = GetComponent<BasicMovement>();
        basicMovement.Move();
    }
}
