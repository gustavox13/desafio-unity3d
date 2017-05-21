using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartScript : MonoBehaviour {

    public Button startButton;

    void Start()
    {
        startButton = startButton.GetComponent<Button>();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }



}
