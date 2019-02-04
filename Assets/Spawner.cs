
using UnityEngine;

public class Spawner : MonoBehaviour {
    public GameObject prefab;
    public float spawnInterval;
    public AudioClip spawnNoise;
    private float timeToSpawn;
    private Flash m_Flash;
    private AudioSource m_AudioSource;

    void Start ()
    {
        timeToSpawn = spawnInterval;
        m_Flash = GetComponent<Flash>();
        m_AudioSource = GetComponent<AudioSource>();
    }

    void OnGUI()
    {
        Vector3 screenPos = MyGlobal.WorldToScreen(transform.position);
        GUIStyle s = new GUIStyle();
        s.normal.textColor = new Color(1.0f, 0.0f, 0.0f);
        //GUI.Label(new Rect(screenPos.x, Screen.height - screenPos.y, 100, 100), timeToSpawn.ToString() ,s);
    }

    private void FixedUpdate()
    {
        timeToSpawn -= Time.fixedDeltaTime;
        if(timeToSpawn <= 0)
        {
            spawn();
            timeToSpawn = spawnInterval;
        }
        if(timeToSpawn <= 1.0f)
        {
            m_Flash.SetIsFlashing(true);
        }
        else
        {
            m_Flash.SetIsFlashing(false);
        }
    }

    private void spawn()
    {
        m_AudioSource.PlayOneShot(spawnNoise);
        Instantiate(prefab, GetComponent<Transform>().position, Quaternion.identity);
    }
}
