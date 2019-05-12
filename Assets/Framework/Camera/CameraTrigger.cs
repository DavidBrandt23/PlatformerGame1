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
    private void Start()
    {
        cameraMoveScript = camera.GetComponent<CameraMovement>();
    }
    void OnEnable()
    {
        GetComponent<Collideable>().onCollideDeleage += setCameraSettings;
    }
    void OnDisable()
    {
        GetComponent<Collideable>().onCollideDeleage -= setCameraSettings;
    }

    private void setCameraSettings(GameObject other)
    {
        if(other.tag == "Player")
        {
            cameraMoveScript.setCameraSettings(camSettings);
        }
    }

}
