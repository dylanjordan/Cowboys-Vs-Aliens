using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToTown : MonoBehaviour
{
    public void BackToTown()
    {
        SceneManager.LoadScene("Town");
        Debug.Log("Going back to Town");
    }
}
