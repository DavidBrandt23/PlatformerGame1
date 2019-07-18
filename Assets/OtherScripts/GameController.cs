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
    public GameObject explodeClusterPrefab;
    public GameObject ChatBox;

    public static Vector3? respawnPosition = null;
    private static string currentScene;
    public GameEvent EventSkipToNextLevel;

    public void createExplosionCluster(Transform t, float xSize, float ySize)
    {
        GameObject newEC = Instantiate(explodeClusterPrefab, t);
        ExplosionCluster A = newEC.GetComponent<ExplosionCluster>();
        A.setSize(xSize, ySize);
    }
    #region Public stuff
    public int GetBingoCount()
    {
        return bingoCount;
    }
    public void SetChatMode(bool newVal)
    {
        ChatBox.SetActive(newVal);
        List<GameObject> talkObjects = TalkAnimation.TalkerObjects;

        if (talkObjects != null)
        {
            foreach (GameObject ob in talkObjects)
            {
                ob.GetComponent<TalkAnimation>().setTalkModeEnabled(newVal);
            }
        }
        SetHUDVisibility(!newVal);
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
    public bool HasCheckPoint()
    {
        return (respawnPosition != null);
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
        if(sceneName.Length != 0)
        {

            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.Log("tried to load blank scene name");
        }
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
        
        bool skipLevel = CrossPlatformInputManager.GetButton("SkipLevel");
        if (skipLevel)
        {
            EventSkipToNextLevel.Raise();
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
