using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class DialogueBoxController : MonoBehaviour
{
    List<String> messages;
    public TextMeshProUGUI speakerText;
    public TextMeshProUGUI mainText;
    public int currentMessage = 0;
    public GameEvent nextMessageEvent;
    private float textTimer;
    private const float textSpeed = 0.5f;
    private DialogScript currentScript;
    public GameObject arrow;
    
    // Start is called before the first frame update
    void Start()
    {
        //messages = new List<String>();
        //messages.Add("What is going on here?");
        //messages.Add("Those creatures stole all the ingredients for Swamp Water!");
        //messages.Add("I'll get those ingredients back!");
        speakerText.text = "";
        mainText.text = "";
        setTextToCurrentMessage();
    }

    public void displayNextMessage()
    {
        if (!readyToAdvance())
        {
            return;
        }
        currentMessage++;
        textTimer = 0.0f;
        if (currentMessage >= currentScript.getNumLines())
        {
            currentScript.OnDialogComplete.Invoke();
            currentScript = null;
            enabled = false;
            return;
        }
        //setTextToCurrentMessage();
    }
    public void StartDialog(DialogScript s)
    {
        currentScript = s;
        currentMessage = 0;
        textTimer = 0.0f;
    }

    private void setTextToCurrentMessage()
    {
    }

    private string getDisplayMessage()
    {
        if (currentScript == null || (currentScript.getNumLines() <= currentMessage))
        {
            return "";
        }
        string transStart = "<color=#ffffff00>";
        string transEnd = "</color>";
        string baseMessage = currentScript.getLines(currentMessage);
        int numLetters = Math.Min((int)textTimer, baseMessage.Length);
        return baseMessage.Substring(0, numLetters) + transStart + baseMessage.Substring(numLetters, baseMessage.Length - (numLetters)) + transEnd;
    }

    public bool readyToAdvance()
    {
        return (textTimer > currentScript.getLines(currentMessage).Length);
    }

    private void FixedUpdate()
    {
        textTimer += textSpeed;
        speakerText.text = currentScript.getSpeakerName(currentMessage);
        mainText.text = getDisplayMessage();
        arrow.SetActive(readyToAdvance());
    }

    // Update is called once per frame
    void Update()
    {
        bool jumpPressed = CrossPlatformInputManager.GetButtonDown("Jump");
        if (jumpPressed)
        {
            nextMessageEvent.Raise();
        }
    }
}
