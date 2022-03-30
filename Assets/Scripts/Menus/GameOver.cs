using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    // mainly placeholder, just want to get the endgame scene in
    // will change to trigger by player death later
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene("GameOver");
            UnityEngine.Cursor.visible = true;
            Debug.Log("Game Over");
        }
        else
        {
            Time.timeScale = 1.0f;
        }
    }
}
