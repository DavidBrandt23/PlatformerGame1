using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collideable : MonoBehaviour {

    public delegate void CollideDeleage(GameObject g);
    public CollideDeleage onCollideDeleage;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    internal void InformCollision(GameObject b)
    {
        if(onCollideDeleage != null)
        {
            onCollideDeleage(b);
        }
    }
}
