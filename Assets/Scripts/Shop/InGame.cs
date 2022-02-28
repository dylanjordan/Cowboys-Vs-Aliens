using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGame : MonoBehaviour
{
    public PlayerMovement _player;
    public int shopToLoad = 0;
    [SerializeField] GameObject indicator;

    private void Start()
    {
      
        indicator.GetComponent<GameObject>();
        indicator.SetActive(false);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            indicator.SetActive(true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (_player.GetShopInput())
            {
                Debug.Log("yer");
                SceneManager.LoadScene(shopToLoad);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        indicator.SetActive(false);
    }
}
