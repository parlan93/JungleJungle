using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StoreController : MonoBehaviour {

    public Text PlayerBananasTxt;
    Text playerBananasTxt;

    int playerBananas;
    int updatedPlayerBananas;

    int shieldLevel;
    int doubleBananasLevel;
    int doubleTaskLevel;

    int shieldCost;
    int doubleBananasCost;
    int doubleTaskCost;
    int currentLevelCost;
    public Text ShieldCostTxt;
    public Text DoubleBananasCostTxt;
    public Text DoubleTaskCostTxt;
    Text shieldCostTxt;
    Text doubleBananasCostTxt;
    Text doubleTaskCostTxt;

    public GameObject[] shieldGreyStars;
    public GameObject[] shieldYellowStars;
    public GameObject[] doubleBananasGreyStars;
    public GameObject[] doubleBananasYellowStars;
    public GameObject[] doubleTaskGreyStars;
    public GameObject[] doubleTaskYellowStars;

    // Use this for initialization
    void Start () {
        playerBananasTxt = PlayerBananasTxt.GetComponent<Text>();
        playerBananas = PlayerPrefs.GetInt("Bananas");
        playerBananasTxt.text = playerBananas.ToString();

        // Get PowerUp Levels
        shieldLevel = GetPowerUpLevel("PU-Shield-Level");
        doubleBananasLevel = GetPowerUpLevel("PU-Bananas-Level");
        doubleTaskLevel = GetPowerUpLevel("PU-Double-Level");
        
        // Upgrades cost calculations
        shieldCostTxt = ShieldCostTxt.GetComponent<Text>();
        doubleBananasCostTxt = DoubleBananasCostTxt.GetComponent<Text>();
        doubleTaskCostTxt = DoubleTaskCostTxt.GetComponent<Text>();
        currentLevelCost = 200;
        for (int i = 1; i < 6; i++)
        {
            currentLevelCost *= i;
            if (shieldLevel == i) shieldCost = currentLevelCost;
            if (doubleBananasLevel == i) doubleBananasCost = currentLevelCost;
            if (doubleTaskLevel == i) doubleTaskCost = currentLevelCost;
        }
        shieldCostTxt.text = shieldCost.ToString();
        doubleBananasCostTxt.text = doubleBananasCost.ToString();
        doubleTaskCostTxt.text = doubleTaskCost.ToString();

        // Show/Hide Grey Stars
        showHideGreyStars(shieldGreyStars, "PU-Shield-Level");
        showHideGreyStars(doubleBananasGreyStars, "PU-Bananas-Level");
        showHideGreyStars(doubleTaskGreyStars, "PU-Double-Level");

        // Show/Hide Yellow Stars
        showHideYellowStars(shieldYellowStars, "PU-Shield-Level");
        showHideYellowStars(doubleBananasYellowStars, "PU-Bananas-Level");
        showHideYellowStars(doubleTaskYellowStars, "PU-Double-Level");
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        updatedPlayerBananas = PlayerPrefs.GetInt("Bananas");
        if (updatedPlayerBananas != playerBananas)
        {
            playerBananas = updatedPlayerBananas;
            playerBananasTxt.text = playerBananas.ToString();
        }
	}

    // Back to main menu
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // Get PowerUp Level and possibility of upgrade
    int GetPowerUpLevel(string key)
    {
        if (!PlayerPrefs.HasKey(key) || PlayerPrefs.GetInt(key) < 1)
        {
            PlayerPrefs.SetInt(key, shieldLevel);
            return 1;
        }
        else
        {
            if (PlayerPrefs.GetInt(key) == 5)
            {
                Text powerUpUpgrade;
                GameObject powerUpUpgradeCost;
                GameObject powerUpUpgradeIcon;
                if (key == "PU-Shield-Level")
                {
                    powerUpUpgrade = GameObject.Find("PU-S-UpTxt").GetComponent<Text>();
                    powerUpUpgradeCost = GameObject.Find("PU-S-Cost");
                    powerUpUpgradeIcon = GameObject.Find("Cost-S");
                } else if (key == "PU-Bananas-Level")
                {
                    powerUpUpgrade = GameObject.Find("PU-DB-UpTxt").GetComponent<Text>();
                    powerUpUpgradeCost = GameObject.Find("PU-DB-Cost");
                    powerUpUpgradeIcon = GameObject.Find("Cost-DB");
                } else
                {
                    powerUpUpgrade = GameObject.Find("PU-DT-UpTxt").GetComponent<Text>();
                    powerUpUpgradeCost = GameObject.Find("PU-DT-Cost");
                    powerUpUpgradeIcon = GameObject.Find("Cost-DT");
                }
                powerUpUpgrade.text = "MAX";
                powerUpUpgradeCost.SetActive(false);
                powerUpUpgradeIcon.SetActive(false);
            }
            return PlayerPrefs.GetInt(key);
        }
    }

    // PowerUp Upgrade
    public void Upgrade(string key)
    {
        int currentLevel = PlayerPrefs.GetInt(key);
        if (currentLevel < 5)
        {
            int upgradeCost = 200;
            for (int i = 1; i <= currentLevel; i++) upgradeCost *= i;
            if (playerBananas > upgradeCost)
            {
                PlayerPrefs.SetInt(key, currentLevel + 1);
                playerBananas -= upgradeCost;
                PlayerPrefs.SetInt("Bananas", playerBananas);
                playerBananasTxt.text = playerBananas.ToString();
                if (key == "PU-Shield-Level")
                {
                    shieldCostTxt.text = (upgradeCost * (currentLevel + 1)).ToString();
                    showHideGreyStars(shieldGreyStars, key);
                    showHideYellowStars(shieldYellowStars, key);
                    if ((currentLevel + 1) == 5)
                    {
                        Text thisPowerUp = GameObject.Find("PU-S-UpTxt").GetComponent<Text>();
                        thisPowerUp.text = "MAX";
                    }
                }
                else if (key == "PU-Bananas-Level")
                {
                    doubleBananasCostTxt.text = (upgradeCost * (currentLevel + 1)).ToString();
                    showHideGreyStars(doubleBananasGreyStars, key);
                    showHideYellowStars(doubleBananasYellowStars, key);
                }
                else if (key == "PU-Double-Level")
                {
                    doubleTaskCostTxt.text = (upgradeCost * (currentLevel + 1)).ToString();
                    showHideGreyStars(doubleTaskGreyStars, key);
                    showHideYellowStars(doubleTaskYellowStars, key);
                }
            }
        }
    }

    // Show/Hide Grey Stars
    void showHideGreyStars(GameObject[] GOs, string key)
    {
        int currentStar = 1;
        foreach (GameObject GO in GOs)
        {
            if (PlayerPrefs.GetInt(key) < currentStar) GO.SetActive(true);
            else GO.SetActive(false);
            currentStar++;
        }
    }

    // Show/Hide Yellow Stars
    void showHideYellowStars(GameObject[] GOs, string key)
    {
        int currentStar = 1;
        foreach (GameObject GO in GOs)
        {
            if (PlayerPrefs.GetInt(key) >= currentStar) GO.SetActive(true);
            else GO.SetActive(false);
            currentStar++;
        }
    }
}
