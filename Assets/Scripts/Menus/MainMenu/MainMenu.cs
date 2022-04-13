using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public bool newGame;

    public float defaultHealth = 4.0f;
    public int defaultCoins = 0;
    public int defaultSpeed = 15;

   public void PlayGame()
    {
        SceneManager.LoadScene("Level1");

        PlayerPrefs.SetFloat("_maxHealth", defaultHealth);
        PlayerPrefs.SetInt("coinAmount", defaultCoins);
        PlayerPrefs.SetInt("maxSpeed", defaultSpeed);

        newGame = true;
    }

    public void Tutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    // function to close the game
    public void QuitGame()
    {
        // to show it works without needing to build
        Debug.Log("Exiting Game...");
        Application.Quit();
    }
}
