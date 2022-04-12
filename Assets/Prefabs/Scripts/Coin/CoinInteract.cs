using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinInteract : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CoinCounter.coinAmount++;
            Destroy(gameObject);
        }
    }
}
