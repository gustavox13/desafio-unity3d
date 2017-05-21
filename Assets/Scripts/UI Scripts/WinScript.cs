using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinScript : MonoBehaviour {

    public GameObject Box;
    public Canvas WinScreen;
    public Button restartButton;
    public Button menuButton;

    private void Start()
    {
        WinScreen.enabled = false;
        restartButton = restartButton.GetComponent<Button>();
        menuButton = menuButton.GetComponent<Button>();
        Time.timeScale = 1.0f;
  
    }

    void Update () {
	
        if (Box.transform.position.x >= 31)
        {
            Time.timeScale = 0.0f;
            WinScreen.enabled = true;
        }	

	}

    public void RestartGame()
    {
          SceneManager.LoadScene("Game");
        

    }

    public void MenuGame()
    {
        SceneManager.LoadScene("Start");
    }

}
