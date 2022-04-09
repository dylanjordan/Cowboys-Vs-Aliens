using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ShopManager : MonoBehaviour
{
    Player player;

    public int[,] shopItems = new int[4, 4];
    private int coins;

    public Text coinsText;
    public GameObject NotEnoughMenu;
    public GameObject CoinsCounter;

    public bool bought = false;

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
        shopItems[2, 1] = 50;
        shopItems[2, 2] = 50;
        shopItems[2, 3] = 5;

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
        bought = true;
    }

    public void NotEnoughCoins()
    {
        StartCoroutine(NotEnoughMenuActive());
    }

    IEnumerator NotEnoughMenuActive()
    {
        GameObject ButtonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;
        if (CoinCounter.coinAmount < shopItems[2, ButtonRef.GetComponent<ButtonInfo>().ItemID])
        {
            Debug.Log("You do not have enough coins to buy this!");
            NotEnoughMenu.SetActive(true);
        }
        else if (CoinCounter.coinAmount >= shopItems[2, ButtonRef.GetComponent<ButtonInfo>().ItemID])
        {
            NotEnoughMenu.SetActive(false);
        }
        yield return new WaitForSeconds(1.0f);
        NotEnoughMenu.SetActive(false);
    }
    public void WinGame()
    {
        SceneManager.LoadScene("WinGame");
    }

    IEnumerator GoToWinGame()
    {
        Buy();
        yield return new WaitForSeconds(0.01f);
        WinGame();
    }

    public void BuyVictory()
    {
        StartCoroutine(GoToWinGame());
    }

    public void BuyHealth()
    {
        player._maxHealth += healthIncrease;
    }
}



