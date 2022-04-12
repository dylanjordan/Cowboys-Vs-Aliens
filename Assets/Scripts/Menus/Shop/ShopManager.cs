using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ShopManager : MonoBehaviour
{
    public int[,] shopItems = new int[4, 4];
    private int coins;

    public Text coinsText;
    public GameObject NotEnoughMenu;
    public GameObject CoinsCounter;

    int healthIncrease = 2;

    // Start is called before the first frame update
    void Start()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        CoinCounter.coinAmount = data.coins;
        coins = CoinCounter.coinAmount;
        coinsText.text = coins.ToString();

        //IDS
        shopItems[1, 1] = 1;
        shopItems[1, 2] = 2;
        shopItems[1, 3] = 3;

        //Price(s)
        shopItems[2, 1] = 2;
        shopItems[2, 2] = 0;
        shopItems[2, 3] = 0;

    }

    public void Buy()
    {
        GameObject ButtonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;

        // get the price of the item selected
        if (CoinCounter.coinAmount >= shopItems[2, ButtonRef.GetComponent<ButtonInfo>().ItemID])
        {
            // remove amount of coins item costs
            CoinCounter.coinAmount -= shopItems[2, ButtonRef.GetComponent<ButtonInfo>().ItemID];
            // update coins text
            coinsText.text = CoinCounter.coinAmount.ToString();
        }
        if (CoinCounter.coinAmount < shopItems[2, ButtonRef.GetComponent<ButtonInfo>().ItemID])
        {
            StartCoroutine(NotEnoughMenuActive());
        }
        else if (CoinCounter.coinAmount >= shopItems[2, ButtonRef.GetComponent<ButtonInfo>().ItemID])
        {
            NotEnoughMenu.SetActive(false);
        }
    }

    IEnumerator NotEnoughMenuActive()
    {
        GameObject ButtonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;

        Debug.Log("You do not have enough coins to buy this!");

        NotEnoughMenu.SetActive(true);

        yield return new WaitForSeconds(0.9f);
        NotEnoughMenu.SetActive(false);
    }

    public void BuyHealth()
    {
        Debug.Log("Maximum Health increased by 'x'!");
    }

    public void BuySpeed()
    {
        Debug.Log("Movement Speed Increased by 'x' %!"); // idk wat number so ya
    }

    public void BuyDamage()
    {
        Debug.Log("Attack Damage increased by 'x' %!");
    }
}



