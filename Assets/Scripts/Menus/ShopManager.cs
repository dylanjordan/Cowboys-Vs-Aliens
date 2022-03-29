using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ShopManager : MonoBehaviour
{
    public int[,] shopItems = new int[4, 4];
    public float coins; // change to ingame currency later

    public Text coinsText;

    // Start is called before the first frame update
    void Start()
    {
        coinsText.text = "Coins:" + coins.ToString();

        //IDS
        shopItems[1, 1] = 1;
        shopItems[1, 2] = 2;
        shopItems[1, 3] = 3;

        //Price(s)
        shopItems[2, 1] = 5;
        shopItems[2, 2] = 10;
        shopItems[2, 3] = 9;

    }

    public void Buy()
    {
        GameObject ButtonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;

        // get the price of the item selected
        if (coins >= shopItems[2, ButtonRef.GetComponent<ButtonInfo>().ItemID])
        {
            // remove amount of coins item costs
            coins -= shopItems[2, ButtonRef.GetComponent<ButtonInfo>().ItemID];
            // update coins text
            coinsText.text = "Coins:" + coins.ToString();
            
        }
    }
}
