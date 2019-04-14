using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DevPreload : MonoBehaviour
{
    void Awake()
    {
        //how im doing this now doesn't really work
        GameObject check = GameObject.Find("GameController");
        if (check == null)
        { UnityEngine.SceneManagement.SceneManager.LoadScene("_preload"); }
    }
}
