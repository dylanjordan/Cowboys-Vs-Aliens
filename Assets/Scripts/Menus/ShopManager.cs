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
    public GameObject NotEnoughMenu;

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
        shopItems[2, 2] = 20;
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

    public void NotEnoughCoins()
    {
        StartCoroutine(NotEnoughMenuActive());
    }

    IEnumerator NotEnoughMenuActive()
    {
        GameObject ButtonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;
        if (coins < shopItems[2, ButtonRef.GetComponent<ButtonInfo>().ItemID])
        {
            Debug.Log("You do not have enough coins to buy this!");
            NotEnoughMenu.SetActive(true);
        }
        yield return new WaitForSeconds(1.0f);
        NotEnoughMenu.SetActive(false);
    }
}
