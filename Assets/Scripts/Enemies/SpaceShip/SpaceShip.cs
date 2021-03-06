using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SpaceShip : MonoBehaviour
{
    [SerializeField] EnemyHealthBar _healthBarControl;
    [SerializeField] GameObject _healthBar;

    public ParticleSystem _hitParticles;
    public Rigidbody2D _enemyRb;
    public PolygonCollider2D _enemyPolyCollider;

    public float _enemyHealth = 10.0f;
    public float _enemyMaxHealth = 10.0f;

    public int _damageRecieved = 2;

    public GameObject popUpMessage; // temporary

    public void Awake()
    {
        _enemyRb = GetComponent<Rigidbody2D>();
        _enemyPolyCollider = GetComponent<PolygonCollider2D>();
    }
    void Start()
    {
        ResetHealth();
    }

    void Update()
    {
        UpdateEnemy();
    }

    public void TakeDamage()
    {
        _enemyHealth -= _damageRecieved;
    }

    private void UpdateEnemy()
    {
        KillIfDead();
    }

    private void ResetHealth()
    {
        _enemyHealth = _enemyMaxHealth;
    }

    private void KillIfDead()
    {
        if (CheckIfDead())
        {
            Destroy(gameObject);
            popUpMessage.SetActive(true);
        }
    }

    private bool CheckIfDead()
    {
        if (_enemyHealth <= 0.0f)
        {
            return true;
        }
        return false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        transform.localScale = new Vector2(-(Mathf.Sign(_enemyRb.velocity.x)), transform.localScale.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Gun")
        {
            Instantiate(_hitParticles, transform.position, Quaternion.identity);
            _healthBar.SetActive(true);
            TakeDamage();
            _healthBarControl.SetHealth(_enemyHealth, _enemyMaxHealth);
        }
    }
}
