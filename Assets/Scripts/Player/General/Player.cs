using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //create player health

    public float _maxHealth = 4.0f;
    public float _currentHealth;

    public HealthbarUI healthBar;

    // Start is called before the first frame update
    void Awake()
    {
        _currentHealth = _maxHealth;
        healthBar.SetMaxHealth(_maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        Die();
    }

    private void Die()
    {
        if (_currentHealth <= 0)
        {
            Destroy(gameObject);
            SceneManager.LoadScene("GameOver");
            UnityEngine.Cursor.visible = true;
            Debug.Log("Game Over");
        }
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        healthBar.SetHealth(_currentHealth);
        Debug.Log("Player's current health is " + _currentHealth);
    }

    //make player take damage

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            TakeDamage(2);
        }
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        _currentHealth = data.health;
        CoinCounter.coinAmount = data.coins;

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
    }
}
