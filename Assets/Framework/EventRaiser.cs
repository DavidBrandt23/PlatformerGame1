using UnityEngine;
using System.Collections;

public class EventRaiser : MonoBehaviour
{
    public GameEvent gameEvent;
    private void OnEnable()
    {
        gameEvent.Raise();
        
    }


}
