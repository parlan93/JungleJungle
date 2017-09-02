using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;

public class Ads : MonoBehaviour
{
    void Start()
    {
        Advertisement.Initialize("1451665", true);

        StartCoroutine(ShowAdWhenReady());
    }

    IEnumerator ShowAdWhenReady()
    {
        while(!Advertisement.IsReady())
        {
            yield return null;
        }

        Advertisement.Show();
    }

    /*public void ShowAd()
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show();
        }
    }*/
}


