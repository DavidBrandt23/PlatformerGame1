using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class CallResponseOnAwake : MonoBehaviour
{
    public UnityEvent Response;
    // Use this for initialization
    void Start()
    {
        Response.Invoke();
    }
    
}
