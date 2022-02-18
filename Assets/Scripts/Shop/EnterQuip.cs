using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterQuip : MonoBehaviour
{
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

    private void OnTriggerExit2D(Collider2D collision)
    {
        indicator.SetActive(false);
    }
}
