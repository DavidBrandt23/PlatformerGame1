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
    private int bingoCount = 0;
    public Vector3? respawnPosition = null;
    private string currentScene;
    public int GetBingoCount()
    {
        return bingoCount;
    }

    // Start is called before the first frame update
    void Start()
    {
        pauseCanvas.SetActive(false);
        paused = false;
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
        string startScene = "Scene1";
        currentScene = startScene;
        if (SceneManager.GetActiveScene().name.Equals(startScene))
        {
            LevelMap.GetLevelMapObject().onLoadLevel();
        }
        else
        {
            loadLevel(startScene);
        }
        
    }
    public void OnPlayerDeath()
    {
        gameOverCanvas.SetActive(true);
        Invoke("respawn", 3.0f);
    }

    private void respawn()
    {
        loadLevel(currentScene);
    }
    private void OnSceneLoaded(Scene aScene, LoadSceneMode aMode)
    {
        LevelMap.GetLevelMapObject().onLoadLevel();
        gameOverCanvas.SetActive(false);
        if (respawnPosition != null)
        {
           MyGlobal.GetPlayerObject().transform.position = (Vector3)respawnPosition;

        }
    }

    private void loadLevel(string sceneName)
    {

        SceneManager.LoadScene(sceneName);

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
    public void AddBingoCard()
    {
        bingoCount += 1;
    }
    public void UnPause()
    {
        paused = false;
        this.onUnPause();
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

    public void QuitGame()
    {
        Application.Quit();
    }
}
