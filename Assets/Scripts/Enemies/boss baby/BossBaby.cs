using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BossBaby : MonoBehaviour
{
    [Header("Health Related")]
    [SerializeField] EnemyHealthBar _healthBarControl;
    [SerializeField] GameObject _healthBar;

    public ParticleSystem _hitParticles;
    public Rigidbody2D _enemyRb;
    public float _enemyHealth = 10.0f;
    public float _enemyMaxHealth = 10.0f;
    public float _contactDamage = 0.5f;
    public int _damageRecieved = 2;

    [Header("Movement Related")]

    public Transform _playerLocation;
    public Transform _playerModel;
    public Transform _enemyGFX;
    public Transform shootPos;

    private Path _path;
    private Seeker _seeker;
    private Rigidbody2D _rb;

    private int _currentWaypoint = 0;

    private bool _reachedEndOfPath = false;
    private bool _isMoving = false;

    public float _enemySpeed = 200f;
    public float _nextWaypointDistance = 2f;
    public float range = 15.0f;

    private bool _isFacingRight = false;

    [SerializeField] public float _UpdatePathRate = .5f;
    [SerializeField] public float _distanceVal = 5.0f;


    [Header("Attack Related")]
    public GameObject _bulletPrefab;

    private bool _isShooting = false;
    private bool _isInShootingRange = false;
    private bool canShoot = true;

    public float _shootRange, _shootSpeed;
    public float _timeBeforeNextShot = 1.0f;

    [Header("Other")]
    public PolygonCollider2D _enemyPolyCollider;
    public Animator animator;

    public void Awake()
    {
        _enemyRb = GetComponent<Rigidbody2D>();
        _enemyPolyCollider = GetComponent<PolygonCollider2D>();
        _seeker = GetComponent<Seeker>();
        _rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        ResetHealth();
        InvokeRepeating("UpdatePath", 0f, _UpdatePathRate);
    }

    void Update()
    {
        UpdateEnemy();

        animator.SetBool("IsMoving", _isMoving);
    }

    private void UpdatePath()
    {
        if (_seeker.IsDone())
        {
            _seeker.StartPath(_rb.position, _playerLocation.position, OnPathComplete);
        }
    }

    void OnPathComplete(Path path)
    {
        if (!path.error)
        {
            _path = path;
            _currentWaypoint = 0;
        }
    }

    private void FixedUpdate()
    {
        FlipTowardsPlayer();

        if (Vector3.Distance(_playerLocation.position, transform.position) <= range || _isShooting)
        {
            FindPath();
        }

        if (Vector3.Distance(_playerLocation.position, transform.position) <= _shootRange && canShoot)
        {
            _isShooting = true;
            _isInShootingRange = true;
            StartCoroutine(Shoot());
        }
        if (Vector3.Distance(_playerLocation.position, transform.position) >= _shootRange)
        {
            _isInShootingRange = false;
        }
        else
        {
            _isShooting = false;
        }
    }

    private void FindPath()
    {
        if (_path == null)
        {
            return;
        }

        if (_currentWaypoint >= _path.vectorPath.Count)
        {
            _reachedEndOfPath = true;
            return;
        }
        else
        {
            _reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)_path.vectorPath[_currentWaypoint] - _rb.position).normalized;
        Vector2 force = direction * _enemySpeed * Time.deltaTime;

        _rb.AddForce(force);

        float distance = Vector2.Distance(_rb.position, _path.vectorPath[_currentWaypoint]);

        if (distance < _nextWaypointDistance)
        {
            _currentWaypoint++;
        }
        if (_rb.velocity.x >= 0.01f && force.x > 0f)
        {
            //_enemyGFX.localScale = new Vector3(-0.1f, 0.1f, 1f);
            _isMoving = true;
        }
        else if (_rb.velocity.x <= -0.0f && force.x < 0f)
        {
            //_enemyGFX.localScale = new Vector3(0.1f, 0.1f, 1f);
            _isMoving = true;
        }

        if (_rb.velocity.x == 0.0f && force.x > 0.0f)
        {
            _isMoving = false;
        }
    }

    IEnumerator Shoot()
    {
        canShoot = false;
        yield return new WaitForSeconds(_timeBeforeNextShot);
        GameObject newBullet = Instantiate(_bulletPrefab, shootPos.position, Quaternion.Euler(new Vector3(0, 0, 0)));

        newBullet.GetComponent<Rigidbody2D>().velocity = (_playerModel.position - transform.position).normalized * _shootSpeed;
        canShoot = true;
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

        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            player.TakeDamage(_contactDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, _shootRange);
        Gizmos.DrawWireSphere(transform.position, range);
    }

    public bool GetIsShooting()
    {
        return _isShooting;
    }

    public bool GetShootingRange()
    {
        if (Vector3.Distance(_playerLocation.position, transform.position) <= range)
        {
            return true;
        }
        else
            return false;
    }
    void FlipTowardsPlayer()
    {
        float distFromPlayer = _playerModel.position.x - transform.position.x;

        if (distFromPlayer < 0 && _isFacingRight)
        {
            Flip();
        }
        else if (distFromPlayer > 0 && !_isFacingRight)
        {
            Flip();
        }
    }
    void Flip()
    {
        _isFacingRight = !_isFacingRight;
        transform.Rotate(0, 180, 0);
    }

}
