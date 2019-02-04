using UnityEngine;
using System.Collections;

public class DestroyOnTimer : MonoBehaviour
{
    [SerializeField] private bool onAwake = false;
    public float TotalTime = 1.0f;
    // Use this for initialization
    void Start()
    {
        if (onAwake)
        {
            this.StartTimer();
        }
    }
    public void StartTimer()
    {
        Destroy(this.gameObject, TotalTime);
    }
}
