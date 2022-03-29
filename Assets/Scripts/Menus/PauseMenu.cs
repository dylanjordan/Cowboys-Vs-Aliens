using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject optionMenu;
    public GameObject settingsMenu;
    public GameObject controlsMenu;

    public static bool isPaused = false;
    public static bool isOption = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            { 
                // press escape when paused -> back to game
                ResumeGame();
            }
            else
            {
                // press escape not paused -> pause game
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        // pause menu is active, game is paused
        pauseMenu.SetActive(true);
        Time.timeScale = 0.0f;
        isPaused = true;
        
        if (settingsMenu.activeInHierarchy)
        {
            settingsMenu.SetActive(false);
        }
        if (controlsMenu.activeInHierarchy)
        {
            controlsMenu.SetActive(false);
        }
        if (optionMenu.activeInHierarchy)
        {
            optionMenu.SetActive(false);
        }
    }

    public void ResumeGame()
    {
        // resumes the game
        pauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
        isPaused = false;
    }

    public void GoToMainMenu()
    {
        // set the scene to main menu
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainMenu");
        isPaused = false;
    }

}
