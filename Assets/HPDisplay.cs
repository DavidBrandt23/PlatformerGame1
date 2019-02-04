using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPDisplay : MonoBehaviour {

    public GameObject targetObject;
    Text guiText;
	// Use this for initialization
	void Start () {
        guiText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		if(targetObject == null)
        {
            guiText.text = "";
            return;
        }
        HealthScript hpScript = targetObject.GetComponent<HealthScript>();
        if(hpScript == null)
        {
            guiText.text = "no hp script on target";
            return;
        }
        guiText.text = "HP: " + hpScript.HealthString();
    }
}
