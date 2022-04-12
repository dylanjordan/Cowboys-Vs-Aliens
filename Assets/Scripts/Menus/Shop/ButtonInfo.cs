using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInfo : MonoBehaviour
{
    public int ItemID;
    public Text PriceText;

    public GameObject ShopManager;

    // Update is called once per frame
    void Update()
    {
        // get the price of the item for the button
        PriceText.text = ShopManager.GetComponent<ShopManager>().shopItems[2, ItemID].ToString();

    }
}


