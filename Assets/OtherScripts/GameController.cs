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
    public static Vector3? respawnPosition = null;
    private string currentScene;
    public RuntimeSet_GameObject NonGridBlocks;
    public bool CutsceneMode;


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

        SceneManager.LoadScene(sceneName);

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
        if (CutsceneMode)
        {
            SetHUDVisibility(false);
        }
    }


    void Start()
    {
        pauseCanvas.SetActive(false);
        paused = false;
       // DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
        string startScene = "Level1";
        //startScene = "Cutscene_Opening";
        currentScene = startScene;
        LevelMap.GetLevelMapObject().onLoadLevel();
        if (SceneManager.GetActiveScene().name.Equals(startScene))
        {
           // LevelMap.GetLevelMapObject().onLoadLevel();
        }
        else
        {
           // loadLevel(startScene);
        }
        
    }

    private void respawn()
    {
        loadLevel(currentScene);
    }
    private void OnSceneLoaded(Scene aScene, LoadSceneMode aMode)
    {
        LevelMap.GetLevelMapObject().onLoadLevel();
        //gameOverCanvas.SetActive(false);
        if (respawnPosition != null)
        {
           MyGlobal.GetPlayerObject().transform.position = (Vector3)respawnPosition;

        }
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
