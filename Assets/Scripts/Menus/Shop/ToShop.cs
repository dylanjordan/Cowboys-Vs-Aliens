using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToShop : MonoBehaviour
{
    public GameObject popUpMessage;
    // this is mainly temporary for the playtest
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T) && popUpMessage.activeInHierarchy)
        {
            SceneManager.LoadScene("Shop");
        }
    }
}
