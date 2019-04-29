using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueBoxController : MonoBehaviour
{
    List<String> messages;
    public TextMeshProUGUI mainText;
    int currentMessage = 0;
    // Start is called before the first frame update
    void Start()
    {
        messages = new List<String>();
        messages.Add("What is going on here?");
        messages.Add("Those creatures stole all the ingredients for Swamp Water!");
        messages.Add("I'll get those ingredients back!");
        setTextToCurrentMessage();
    }
    public void displayNextMessage()
    {
        currentMessage++;
        setTextToCurrentMessage();
    }

    private void setTextToCurrentMessage()
    {
        if(messages.Count <= currentMessage)
        {
            return;
        }
        mainText.text = messages[currentMessage];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
