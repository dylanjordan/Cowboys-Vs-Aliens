using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public float health;
    public int coins;
    public float newSpeed;
    public float[] position;
    
    public PlayerData(Player player)
    {
        health = player._currentHealth;
        coins = CoinCounter.coinAmount;


        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
    }
}



