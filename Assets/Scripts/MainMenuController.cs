using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // Buttons Listeners
    public void PlayGameBtnAction()
    {
        SceneManager.LoadScene("MapSelection");
    }

    public void StoreBtnAction()
    {
        SceneManager.LoadScene("Store");
    }

    public void ExitGameBtnAction()
    {
        Application.Quit();
    }
}
