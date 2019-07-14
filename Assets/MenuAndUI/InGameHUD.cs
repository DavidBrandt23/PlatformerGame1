using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameHUD : MonoBehaviour
{
    public IntegerVariable PlayerHealth;
    public IntegerVariable BingoCount;
    public FloatVariable PlayerEnergy;
    private GameObject UIHolder;
    private GameObject PowerBarUI;
    private bool active = true;
    // Start is called before the first frame update
    void OnEnable()
    {
        UIHolder = GameObject.Find("UIHolder");
        PowerBarUI = GameObject.Find("PowerBarUI");
    }
    public void SetVisibility(bool visible)
    {
        UIHolder.SetActive(visible);
        active = visible;
    }

    // Update is called once per frame
    void Update()
    {
        if (!active)
        {
            return;
        }
        int maxHP = 3;
        int curHP = 0;
        
        if(BingoCount != null)
        {
            int bingoCount = BingoCount.Value;
            GameObject bingoText = GameObject.Find("BingoCountText");
            bingoText.GetComponent<Text>().text = "x" + bingoCount;
        }

        if(PlayerEnergy != null)
        {
            GameObject energyText = GameObject.Find("EnergyText");
            energyText.GetComponent<Text>().text = "Energy: " + PlayerEnergy.Value;
            
            float curEnergy = PlayerEnergy.Value;
            bool hasAnyEnergy = curEnergy > 0;

            PowerBarUI.SetActive(hasAnyEnergy);

            if (hasAnyEnergy)
            {
                GameObject PowerBarFill = GameObject.Find("PowerBarFill");
                PowerBarFill.GetComponent<Image>().fillAmount = (curEnergy / 100.0f);
            }

        }
        if (PlayerHealth != null)
        {
            curHP = PlayerHealth.Value;
        }

        for (int i = 0; i < maxHP; i++)
        {
            int heartIndex = i + 1;
            GameObject heartObject = GameObject.Find("Heart" + heartIndex);
            heartObject.GetComponent<SpriteRenderer>().enabled = (curHP >= heartIndex);
        }

    }
}
