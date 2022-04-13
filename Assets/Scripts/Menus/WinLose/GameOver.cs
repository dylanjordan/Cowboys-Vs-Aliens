using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public int previousScene;
    public int currentLevel;
 
    public void LevelLost()
    {
        SceneManager.LoadScene(previousScene);
    }

    public void LevelWon()
    {
        SceneManager.LoadScene(currentLevel + 1);
    }
}
