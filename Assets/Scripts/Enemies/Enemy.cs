using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] TestHealthbar _healthBarControl;
    
    [SerializeField] GameObject _healthBar;
    
    [SerializeField]  float _deathTimer = 0.3f;

    Rigidbody2D _rb;

    Transform _trans;

    public ParticleSystem _hitParticles;

    public float _enemyHealth = 10.0f;
    public float _enemyMaxHealth = 10.0f;
    public float _enemySpeed = 1.5f;
    public float _contactDamage = 0.5f;
    public float _timer = 5f;

    private float _timeSinceReveal = 0.0f;

    private bool _movingLeft;

    public int _damageRecieved = 2;
    private void Awake()
    {
        _trans = GetComponent<Transform>();
        _rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        ResetHealth();
    }

    private void Move()
    {
        if (_movingLeft)
        {
            transform.position += Vector3.left * Time.deltaTime * _enemySpeed;
        }
        else
        {
            transform.position += Vector3.right * Time.deltaTime * _enemySpeed;
        }
    }
    public void TakeDamage()
    {
        _enemyHealth -= _damageRecieved;
    }

    public virtual void UpdateEnemy()
    {
        KillIfDead();
    }

    public void ResetHealth()
    {
        _enemyHealth = _enemyMaxHealth;
    }

    public void KillIfDead()
    {
        if (CheckIfDead())
        {
            Destroy(gameObject, _deathTimer);
        }
    }
    public bool CheckIfDead()
    {
        if (_enemyHealth <= 0.0f)
        {
            return true;
        }
        return false;
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
        if (collision.collider.tag == "Player")
        {
            _rb.velocity = new Vector2(0, _rb.velocity.y);
        }
        if (collision.gameObject.tag == "Wall")
        {
            _enemySpeed = _enemySpeed * -1;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            _rb.velocity = new Vector2(0, _rb.velocity.y);
        }

        if (collision.collider.tag == "Gun")
        {
            _rb.AddForce(transform.up * 10, ForceMode2D.Impulse);
            _rb.AddForce(transform.right * 10, ForceMode2D.Impulse);

            _timeSinceReveal += Time.deltaTime;
            if (_timeSinceReveal >= _timer)
            {
                _timeSinceReveal = 0.0f;
                _healthBar.SetActive(false);

            }
        }
    }
}
