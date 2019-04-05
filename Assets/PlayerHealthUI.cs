using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int maxHP = 3;
        int curHP = 0;
        GameObject gameCon = MyGlobal.GetGameControllerObject();
        int bingoCount = gameCon.GetComponent<GameController>().GetBingoCount();
        GameObject bingoText = GameObject.Find("BingoCountText");
        bingoText.GetComponent<Text>().text = "x" + bingoCount;
        GameObject player = MyGlobal.GetPlayerObject();
        if(player != null)
        {
            HealthScript hpScript = player.GetComponent<HealthScript>();
            if (hpScript != null)
            {
                curHP = hpScript.CurrentHP;
            }
        }

        for (int i = 0; i < maxHP; i++)
        {
            int heartIndex = i + 1;
            GameObject heartObject = GameObject.Find("Heart" + heartIndex);
            heartObject.GetComponent<SpriteRenderer>().enabled = (curHP >= heartIndex);
        }
    }
}
