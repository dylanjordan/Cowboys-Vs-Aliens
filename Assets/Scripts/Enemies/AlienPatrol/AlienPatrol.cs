using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienPatrol : MonoBehaviour
{
    [Header("Stats Related")]
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

    [Header("For Patrolling")]
    [SerializeField] float _circleRadius;

    [SerializeField] Transform groundCheck;
    [SerializeField] Transform wallCheck;

    [SerializeField] LayerMask groundLayer;

    private float _moveDir = 1;

    private bool _facingRight = true;
    private bool checkingGround;
    private bool checkingWall;

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
    }

    private void FixedUpdate()
    {
        checkingGround = Physics2D.OverlapCircle(groundCheck.position, _circleRadius, groundLayer);
        checkingWall = Physics2D.OverlapCircle(wallCheck.position, _circleRadius, groundLayer);

        Patrolling();
    }

    void Patrolling()
    {
        if (!checkingGround || checkingWall)
        {
            if (_facingRight)
            {
                Flip();
            }
            else if (!_facingRight)
            {
                Flip();
            }
        }
        _enemyRb.velocity = new Vector2(_enemySpeed * _moveDir, _enemyRb.velocity.y);
    }

    void Flip()
    {
        _moveDir *= -1;
        _facingRight = !_facingRight;
        transform.Rotate(0, 180, 0);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            Flip();
            player.TakeDamage(_contactDamage);
        }

        if (collision.collider.tag == "Gun")
        {
            Instantiate(_hitParticles, transform.position, Quaternion.identity);
            _healthBar.SetActive(true);
            TakeDamage();
            _healthBarControl.SetHealth(_enemyHealth, _enemyMaxHealth);
        }

        if (collision.gameObject.layer != LayerMask.NameToLayer("Player") && collision.collider.tag != "Gun")
        {
            Flip();
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.position, _circleRadius);
        Gizmos.DrawWireSphere(wallCheck.position, _circleRadius);
    }
}
