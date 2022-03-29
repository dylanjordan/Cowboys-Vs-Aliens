using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienPatrol : MonoBehaviour
{
    [SerializeField] EnemyHealthBar _healthBarControl;

    [SerializeField] GameObject _healthBar;

    public ParticleSystem _hitParticles;
    public Rigidbody2D _enemyRb;
    public BoxCollider2D _enemyBoxCollider;

    public float _enemyHealth = 10.0f;
    public float _enemyMaxHealth = 10.0f;
    public float _enemySpeed = 1f;
    public float _contactDamage = 0.5f;

    public int _damageRecieved = 2;

    private void Awake()
    {
        _enemyRb = GetComponent<Rigidbody2D>();
        _enemyBoxCollider = GetComponent<BoxCollider2D>();
    }
    private void Start()
    {
        ResetHealth();
    }
    private void Update()
    {
        UpdateEnemy();

        if (IsFacingRight())
        {
            _enemyRb.velocity = new Vector2(_enemySpeed, 0f);
        }
        else
        {
            _enemyRb.velocity = new Vector2(-_enemySpeed, 0f);
        }
    }

    private bool IsFacingRight()
    {
        return transform.localScale.x > Mathf.Epsilon;
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
