using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    public string sceneName;
    public float delay;
    // Start is called before the first frame update
    void Start()
    {
    }
    public void loadSceneWithDelay()
    {
        Invoke("loadScene", delay);
    }

    public void loadScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
