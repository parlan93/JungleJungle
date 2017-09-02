using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour {

    [SerializeField]
    string gameID = "1451665";
    int playerBananas;

    void Awake()
    {
        Advertisement.Initialize(gameID, true);
        playerBananas = PlayerPrefs.GetInt("Bananas");
    }

    public void ShowAd(string zone = "")
    {
        if (string.Equals(zone, ""))
            zone = null;

        ShowOptions options = new ShowOptions();
        options.resultCallback = AdCallbackHandler;

        if (Advertisement.IsReady())
            Advertisement.Show(zone, options);
    }

    void AdCallbackHandler(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                Debug.Log("FINISHED");
                playerBananas += 25;
                PlayerPrefs.SetInt("Bananas", playerBananas);
                break;
            case ShowResult.Skipped:
                Debug.Log("Skipped");
                break;
            case ShowResult.Failed:
                Debug.Log("FAILED");
                break;
        }
    }
    
}
