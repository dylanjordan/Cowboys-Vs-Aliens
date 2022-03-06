using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartController : MonoBehaviour
{
    Player player = new Player();

    [SerializeField] private Image[] hearts;

    private void Start()
    {
        player = GetComponent<Player>();
        HeartUpdater();
    }

    public void HeartUpdater()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < player._currentHealth)
            {
                hearts[i].color = Color.red;
            }
            else
            {
                hearts[i].color = Color.black;
            }
        }
    }
}
