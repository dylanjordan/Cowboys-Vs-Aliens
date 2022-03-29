using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Cursor : MonoBehaviour
{
    PauseMenu pauseMenu;

    private void Start()
    {
        UnityEngine.Cursor.visible = false;
    }

    private void Update()
    {
        if (!PauseMenu.isPaused)
        {
            Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = cursorPos;
        }
    }
}
