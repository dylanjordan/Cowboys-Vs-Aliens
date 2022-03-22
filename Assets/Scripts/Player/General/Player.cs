using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //create player health

    public int _maxHealth = 4;
    public int _currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        _currentHealth = _maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentHealth <= 0)
        {
            Destroy(gameObject);
            SceneManager.LoadScene("GameOver");
            UnityEngine.Cursor.visible = true;
            Debug.Log("Game Over");
        }
    }

    //allow player to take damage

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
    }

    //make player take damage

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            TakeDamage(2);
        }
    }
}
