using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashSprite : MonoBehaviour
{
    public float flashInterval;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("flipVisibility", 0.0f, flashInterval);
    }

    private bool visible;
    private void flipVisibility()
    {
        visible = !visible;
        GetComponent<SpriteRenderer>().enabled = visible;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
