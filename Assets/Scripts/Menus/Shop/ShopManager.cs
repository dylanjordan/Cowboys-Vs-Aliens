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

    bool bought = false;

    int playerMaxHealth;
    int healthIncrease = 500;

    int playerDamage;
    int damageIncrease = 1000;

    int playerSpeed;
    int speedIncrease = 2000;

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

    private void Update()
    {
        playerMaxHealth = PlayerPrefs.GetInt("_maxHealth");
        playerDamage = PlayerPrefs.GetInt("_bulletDamage");
        playerSpeed = PlayerPrefs.GetInt("maxSpeed");
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

            bought = true;
        }
        if (CoinCounter.coinAmount < shopItems[2, ButtonRef.GetComponent<ButtonInfo>().ItemID] && bought)
        {
            StartCoroutine(NotEnoughMenuActive());
        }
        else
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
        GainHealth();
        Debug.Log("Maximum Health increased by 'x'!");
    }

    public void BuySpeed()
    {
        GainSpeed();
        Debug.Log("Movement Speed Increased by 'x' %!");
    }

    public void BuyDamage()
    {
        GainDamage();
        Debug.Log("Attack Damage increased by 'x' %!");
    }

    void GainHealth()
    {
        playerMaxHealth += healthIncrease;
        PlayerPrefs.SetInt("_maxHealth", playerMaxHealth);
    }

    void GainDamage()
    {
        playerDamage += damageIncrease;
        PlayerPrefs.SetInt("_bulletDamage", playerDamage);
    }
    void GainSpeed()
    {
        playerSpeed += speedIncrease;
        PlayerPrefs.SetInt("maxSpeed", playerSpeed);
    }
}



