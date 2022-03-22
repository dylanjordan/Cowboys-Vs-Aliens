using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public bool newGame;

   public void PlayGame()
    {
        SceneManager.LoadScene("TestScene");
        newGame = true;
    }

    // function to close the game
    public void QuitGame()
    {
        // to show it works without needing to build
        Debug.Log("Exiting Game...");
        Application.Quit();
    }
}
