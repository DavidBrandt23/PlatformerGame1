using UnityEngine;
using System.Collections;

public class RaiseEventWhenEntered : MonoBehaviour
{

    public GameEvent gameEvent;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("enterrere");
    }
}
