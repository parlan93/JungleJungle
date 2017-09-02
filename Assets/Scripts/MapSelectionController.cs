using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapSelectionController : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GoToLevelSelectionJungle()
    {
        SceneManager.LoadScene("LevelSelection");
    }

    public void GoToLevelSelectionVillage()
    {
        SceneManager.LoadScene("LevelSelectionVillage");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
