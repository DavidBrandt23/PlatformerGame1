using UnityEngine;
using System.Collections;

using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class CameraTrigger : MonoBehaviour
{
    public GameObject camera; //must have camera movement
    private CameraMovement cameraMoveScript;
    public CameraSettings camSettings;
    public bool setOnEnable;
    private void Start()
    {
    }
    void OnEnable()
    {
        cameraMoveScript = camera.GetComponent<CameraMovement>();
        if (setOnEnable)
        {
            setCameraSettings(null);
        }
        else
        {
            GetComponent<Collideable>().onCollideDeleage += setCameraSettings;
        }
    }
    void OnDisable()
    {
        if (setOnEnable)
        {
            return;
        }
        GetComponent<Collideable>().onCollideDeleage -= setCameraSettings;
    }

    private void setCameraSettings(GameObject other)
    {
        if(other == null || (other.tag == "Player"))
        {
            cameraMoveScript.setCameraSettings(camSettings);
        }
    }

}
