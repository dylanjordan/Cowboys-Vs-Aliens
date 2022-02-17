using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : MonoBehaviour
{
    [SerializeField] TestHealthbar healthBarControl;
    [SerializeField] GameObject healthBar;
    public ParticleSystem hitParticles;
    public int _enemyHealth = 10;
    public int _maxHealth = 10;
    public int _damageRecieved = 2;

    private void Start()
    {
        healthBar.SetActive(false);
    }

    private void Update()
    {
        if (_enemyHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Gun")
        {
            Instantiate(hitParticles, transform.position, Quaternion.identity);
            healthBar.SetActive(true);
            TakeDamage();
            healthBarControl.SetHealth(_enemyHealth, _maxHealth);
        }
    }

    public void TakeDamage()
    {
        _enemyHealth -= _damageRecieved;
    }

}
