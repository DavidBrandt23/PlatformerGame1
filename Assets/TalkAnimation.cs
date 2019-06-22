using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkAnimation : MonoBehaviour
{
    public bool isTalking;
    private float startY;
    private const float bounceHeight = 0.15f;
    private float bounceDir = 1.0f;
    private const float bounceSpeed = 0.02f;
    public List<Behaviour> ThingsToDisable;

    public static List<GameObject> TalkerObjects;
    private void OnEnable()
    {
        if(TalkerObjects == null)
        {
            TalkerObjects = new List<GameObject>();
        }
        TalkerObjects.Add(this.gameObject);
    }
    private void OnDisable()
    {
        TalkerObjects.Remove(this.gameObject);
    }
    public void setTalkModeEnabled(bool newVal)
    {
        foreach( Behaviour b in ThingsToDisable)
        {
            b.enabled = !newVal;
        }
    }
    public void setTalking(bool newVal)
    {
        if(isTalking == newVal)
        {
            return;
        }
        isTalking = newVal;

        //start talk
        if (isTalking)
        {
            bounceDir = 1.0f;
            startY = transform.position.y;
        }
        else
        {

            float newY = startY;
            Vector3 curPos = transform.position;
            Vector3 newPos = new Vector3(curPos.x, newY, curPos.z);
            transform.position = newPos;
        }

    }
    private void FixedUpdate()
    {
        if (isTalking)
        {
            float peakY = startY + bounceHeight;
            Vector3 curPos = transform.position;
            float newY = 0.0f;
            newY = curPos.y + bounceSpeed * bounceDir;
            if (bounceDir > 0.0f)
            {
                if(newY >= peakY)
                {
                    bounceDir *= -1;
                }
            }
            else
            {
                if (newY <= startY)
                {
                    bounceDir *= -1;
                }
            }
            Vector3 newPos = new Vector3(curPos.x, newY ,curPos.z);
            transform.position = newPos;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
}
