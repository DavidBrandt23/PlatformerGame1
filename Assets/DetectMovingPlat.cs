using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectMovingPlat : MonoBehaviour
{
    // Update is called once per frame
    private GameObject touchedPlat = null;
    public GameObject getTouchedPlat()
    {
        return touchedPlat;
    }
    private void FixedUpdate()
    {
        GameObject newTouchPlat = null;
        if(MyGlobal.OnGroundObj(this.gameObject, ref newTouchPlat))
        {
            touchedPlat = newTouchPlat;
        }
        else
        {
            touchedPlat = null;
        }
    }
}
