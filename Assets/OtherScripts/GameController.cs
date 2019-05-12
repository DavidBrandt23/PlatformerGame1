using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class GameController : MonoBehaviour
{
    private bool paused;
    public GameObject pauseCanvas;
    public GameObject gameOverCanvas;
    public GameObject HUD;
    private int bingoCount = 0;
    public RuntimeSet_GameObject NonGridBlocks;
    public bool CutsceneMode;

    public static Vector3? respawnPosition = null;
    private static string currentScene;


    #region Public stuff
    public int GetBingoCount()
    {
        return bingoCount;
    }

    public List<GameObject> GetNonGridBlocks()
    {
        return NonGridBlocks.Items;
    }

    public void OnPlayerDeath()
    {
        gameOverCanvas.SetActive(true);
        Invoke("respawn", 3.0f);
    }

    public void loadLevel(string sceneName)
    {
        respawnPosition = null;
        currentScene = sceneName;
        loadScene(sceneName);
    }


    public void AddBingoCard()
    {
        bingoCount += 1;
    }
    public void UnPause()
    {
        paused = false;
        this.onUnPause();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SetHUDVisibility(bool visible)
    {
        HUD.GetComponent<InGameHUD>().SetVisibility(visible);
    }
    #endregion Public stuff

    private void Awake()
    {
    }


    void Start()
    {
        pauseCanvas.SetActive(false);
        paused = false;

        SceneManager.sceneLoaded += OnSceneLoaded;
        string startScene = "Level1";

        currentScene = startScene;
        LevelMap.GetLevelMapObject().onLoadLevel();

        
        if (CutsceneMode)
        {
            SetHUDVisibility(false);
        }

        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName.Equals(currentScene))
        {
            //samescene
            // LevelMap.GetLevelMapObject().onLoadLevel();
           // MyGlobal.GetPlayerObject().transform.position = (Vector3)respawnPosition;
        }
        else
        {

            currentScene = sceneName;
           // respawnPosition = null;
        }

    }

    private void respawn()
    {
        loadScene(currentScene);
    }

    private void loadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    private void OnSceneLoaded(Scene aScene, LoadSceneMode aMode)
    {
        LevelMap.GetLevelMapObject().onLoadLevel();
       // gameOverCanvas.SetActive(false);
        //if (!CutsceneMode && (respawnPosition != null))
        //{
        //    if (!aScene.name.Equals(currentScene))
        //    {
        //        currentScene = aScene.name;
        //        respawnPosition = null;
        //    }
        //    else
        //    {
        //        MyGlobal.GetPlayerObject().transform.position = (Vector3)respawnPosition;
        //    }
        //}
    }

    private void Update()
    {
        bool pausePressed;
        pausePressed = CrossPlatformInputManager.GetButtonDown("Pause");
        if (pausePressed)
        {
            paused = !paused;
            if (paused)
            {
                onPause();
            }
            else
            {
                UnPause();
            }
        }
    }
    private void onPause()
    {
        Time.timeScale = 0;
        pauseCanvas.SetActive(true);
    }

    private void onUnPause()
    {
        Time.timeScale = 1;
        pauseCanvas.SetActive(false);
    }
}
