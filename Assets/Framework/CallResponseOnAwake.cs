using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class CallResponseOnAwake : MonoBehaviour
{
    public UnityEvent Response;
    public float delay;
    // Use this for initialization
    void Start()
    {
        Invoke("callResp", delay);
    }
    private void callResp()
    {
        Response.Invoke();
    }
    
}
