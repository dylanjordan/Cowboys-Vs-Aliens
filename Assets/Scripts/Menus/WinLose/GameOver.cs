using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public int previousScene;

    private void Awake()
    {
        PlayerPrefs.GetInt("previousScene");
    }

    public void LevelLost()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("previousScene"));
    }
}
