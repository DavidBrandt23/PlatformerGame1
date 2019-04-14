using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapToGrid : MonoBehaviour
{
    public float PPU = 16; // pixels per unit (your tile size)

    private void LateUpdate()
    {
        Vector3 position = transform.localPosition;

        position.x = (Mathf.Round(transform.parent.position.x * PPU) / PPU) - transform.parent.position.x;
        position.y = (Mathf.Round(transform.parent.position.y * PPU) / PPU) - transform.parent.position.y;
        Debug.Log("parent x =" + transform.parent.position.x);
        Debug.Log("parent y =" + transform.parent.position.y);
        Debug.Log("child x =" + position.x);
        Debug.Log("child y =" + position.y);
        //transform.localPosition = position;
    }
}