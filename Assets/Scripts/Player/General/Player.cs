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
    void Start()
    {
        _currentHealth = _maxHealth;
        healthBar.SetMaxHealth(_currentHealth);
        FindObjectOfType<GameOver>().previousScene = SceneManager.GetActiveScene().buildIndex;
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
            SceneManager.LoadScene("GameOver");
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        healthBar.SetHealth(_currentHealth);
        Debug.Log("Player's current health is " + _currentHealth);
    }
}
