using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckPoint : MonoBehaviour
{
    public Sprite unTouchedFlag;
    public Sprite touchedFlag;
    private bool isActive = false;
    public AudioClip checkpointNoise;
    public bool debugStartPos;
    // Start is called before the first frame update
    void Start()
    {

        Collideable c = GetComponent<Collideable>();
        c.onCollideDeleage += onCheckPointTouch;
        if(debugStartPos)
        {
            GameObject player = MyGlobal.GetPlayerObject();
            player.transform.position = transform.position;
        }
    }

    private void onCheckPointTouch(GameObject other)
    {
        if (!isActive && other.tag.Equals("Player"))
        {
            isActive = true;
            //SceneManager.LoadScene("Scene2");
            GetComponent<SpriteRenderer>().sprite = touchedFlag;
            GameController.respawnPosition = transform.position;
            MyGlobal.PlayGlobalSound(checkpointNoise);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
