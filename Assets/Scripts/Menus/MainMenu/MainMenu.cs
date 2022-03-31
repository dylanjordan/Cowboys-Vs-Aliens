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
        Debug.Log("Exiting Game..."); // to show it works without needing to build
        Application.Quit();
    }

    // load game button, for now loads player into the town
    public void LoadTown()
    {
        // can change update this later
        SceneManager.LoadScene("Town");
        Debug.Log("Traveling to the Town...");
    }
}
