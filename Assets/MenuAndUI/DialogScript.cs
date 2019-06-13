using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.Events;

public class DialogScript : MonoBehaviour
{
    [SerializeField]
    private List<string> lines;
    [SerializeField]
    private List<GameObject> speakers;
    [SerializeField]
    private List<TalkAnimation> speakerAnimScripts;
    [SerializeField]
    private List<StringReference> speakerNames;

    public UnityEvent OnDialogComplete;
    
    public int getNumLines()
    {
        return lines.Count;
    }
    public string getLines(int i)
    {
        if(i >= lines.Count)
        {
            return "";
        }
        return lines[i];
    }
    public GameObject getSpeaker(int i)
    {
        if (i >= speakers.Count)
        {
            return null;
        }
        return speakers[i];
    }
    public string getSpeakerName(int i)
    {
        if ((speakerNames == null) || (i >= speakerNames.Count))
        {
            return "name not found";
        }
        return speakerNames[i].Value;
    }
    public TalkAnimation getSpeakerScript(int i)
    {
        if ((speakerAnimScripts == null) || (i >= speakerAnimScripts.Count))
        {
            return null;
        }
        return speakerAnimScripts[i];
    }
}
