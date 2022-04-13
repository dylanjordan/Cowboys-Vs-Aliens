using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinInteract : MonoBehaviour
{
    public AudioClip coinSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CoinCounter.coinAmount++;
            AudioSource.PlayClipAtPoint(coinSound, transform.position);
            Destroy(gameObject);
            PlayerPrefs.SetInt("coinAmount", CoinCounter.coinAmount);
        }
    }
}
