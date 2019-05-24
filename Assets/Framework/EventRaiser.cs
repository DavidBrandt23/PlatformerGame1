using UnityEngine;
using System.Collections;

public class EventRaiser : MonoBehaviour
{
    public GameEvent gameEvent;
    public float delaySeconds = 0.0f;
    private void OnEnable()
    {
        Invoke("raise", delaySeconds);
    }
    private void raise()
    {
        gameEvent.Raise();
    }
}
