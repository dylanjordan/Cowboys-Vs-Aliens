using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunEnemy : MonoBehaviour
{
    [Header("Health Related")]
    [SerializeField] EnemyHealthBar _healthBarControl;
    [SerializeField] GameObject _healthBar;

    public ParticleSystem _hitParticles;

    public float _enemyHealth;
    public float _enemyMaxHealth = 7.0f;
    public float _contactDamage = 1.0f;

    public int _damageRecieved = 2;

    [Header("For Patrolling")]
    [SerializeField] float _enemySpeed;
    [SerializeField] float _circleRadius;

    [SerializeField] Transform groundCheck;
    [SerializeField] Transform wallCheck;

    [SerializeField] LayerMask groundLayer;

    private float _moveDir = 1;
    private bool _facingRight = true;
    private bool checkingGround;
    private bool checkingWall;
    private bool _isShooting = false;

    [Header("For Shooting Attack")]
    [SerializeField] Transform _player;
    [SerializeField] Transform _firingPoint;
    [SerializeField] float _attackRate;

    [SerializeField] GameObject _bulletPrefab;

    public int _numBullets = 1;

    public float _spreadAngle = 0;
    public float _bulletSpeed = 15.0f;
    private bool _canAttack = true;

    [Header("For Seeing Player")]
    [SerializeField] Vector2 lineOfSite;
    [SerializeField] LayerMask playerLayer;
    private bool _canSeePlayer;
    [Header("Other")]
    private Rigidbody2D _rb;
    public Animator animator;

    private bool isCurrentlyShooting = false;
    private bool isWalking = false;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        ResetHealth();
    }

    private void Update()
    {
        UpdateShooter();

        animator.SetBool("isShooting", isCurrentlyShooting);
        animator.SetBool("isWalking", isWalking);
    }

    private void FixedUpdate()
    {
        checkingGround = Physics2D.OverlapCircle(groundCheck.position, _circleRadius, groundLayer);
        checkingWall = Physics2D.OverlapCircle(wallCheck.position, _circleRadius, groundLayer);
        _canSeePlayer = Physics2D.OverlapBox(transform.position, lineOfSite, 0, playerLayer);

        if (!_canSeePlayer && _canAttack)
        {
            Patrolling();
        }
        if (_canSeePlayer && _canAttack)
        {
            isWalking = false;
            StartCoroutine(Shoot());
        }
        if (_canSeePlayer)
        {
            FlipTowardsPlayer();
        }
    }

    public void TakeDamage()
    {
        _enemyHealth -= _damageRecieved;
    }

    private void UpdateShooter()
    {
        KillIfDead();
    }

    private void KillIfDead()
    {
        if (CheckIfDead())
        {
            Destroy(gameObject);
        }
    }

    private void ResetHealth()
    {
        _enemyHealth = _enemyMaxHealth;
    }

    private bool CheckIfDead()
    {
        if (_enemyHealth <= 0.0f)
        {
            return true;
        }
        return false;
    }

    void Patrolling()
    {
        isWalking = true;
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
        _rb.velocity = new Vector2(_enemySpeed * _moveDir, _rb.velocity.y);
    }

    void FlipTowardsPlayer()
    {
        float distFromPlayer = _player.position.x - transform.position.x;

        if (distFromPlayer < 0 && _facingRight)
        {
            Flip();
        }
        else if (distFromPlayer > 0 && !_facingRight)
        {
            Flip();
        }
    }

    IEnumerator Shoot()
    {
        _canAttack = false;
        isCurrentlyShooting = true;
        GameObject newBullet = Instantiate(_bulletPrefab, _firingPoint.position, Quaternion.Euler(new Vector3(0, 0, 0)));
        newBullet.GetComponent<Rigidbody2D>().velocity = (_player.position - transform.position).normalized * _bulletSpeed;
        yield return new WaitForSeconds(_attackRate);
        isCurrentlyShooting = false;
        _canAttack = true;
    }
    void Flip()
    {
        _moveDir *= -1;
        _facingRight = !_facingRight;
        transform.Rotate(0, 180, 0);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.position, _circleRadius);
        Gizmos.DrawWireSphere(wallCheck.position, _circleRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, lineOfSite);
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
    }

}
