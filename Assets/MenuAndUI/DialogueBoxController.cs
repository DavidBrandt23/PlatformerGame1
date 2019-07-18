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
    public AudioClip TextBoop;
    
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
            gameObject.SetActive(false);
            return;
        }
        //setTextToCurrentMessage();
    }
    public void StartDialog(DialogScript s)
    {
        gameObject.SetActive(true);
        currentScript = s;
        currentMessage = 0;
        textTimer = 0.0f;
        prevNumLetters = 0;
    }

    private void setTextToCurrentMessage()
    {
    }

    private int prevNumLetters = 0;
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
        if (numLetters > prevNumLetters)
        {
            if (true || altSound)
            {
                MyGlobal.PlayGlobalSound(TextBoop, 0.5f);
                altSound = false;
            }
            else
            {
                altSound = true;
            }
        }
        prevNumLetters = numLetters;
        return baseMessage.Substring(0, numLetters) + transStart + baseMessage.Substring(numLetters, baseMessage.Length - (numLetters)) + transEnd;
    }
    private bool altSound = false;

    private string getSpeakerName()
    {
        if (currentScript == null)
        {
            return "";
        }
        return currentScript.getSpeakerName(currentMessage);
    }
    


    public bool readyToAdvance()
    {
        if(currentScript == null)
        {
            return true;
        }
        return (textTimer > currentScript.getLines(currentMessage).Length);
    }

    private void FixedUpdate()
    {
        if(currentScript == null)
        {
            return;
        }
        textTimer += textSpeed;
        speakerText.text = getSpeakerName();
        mainText.text = getDisplayMessage();
        arrow.SetActive(readyToAdvance());
        if (readyToAdvance())
        {
            TalkAnimation ta = currentScript.getSpeakerScript(currentMessage);
            if(ta != null)
            {
                ta.setTalking(false);
            }
        }
        else
        {
            //not ended
            TalkAnimation ta = currentScript.getSpeakerScript(currentMessage);
            if (ta != null)
            {
                ta.setTalking(true);
            }
        }
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
