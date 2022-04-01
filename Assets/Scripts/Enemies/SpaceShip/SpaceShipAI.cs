using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class SpaceShipAI : MonoBehaviour
{
    public Animator animator;

    public Transform _playerLocation, _playerModel,  _enemyGFX, shootPos;

    public GameObject _bulletPrefab;
    private Path _path;

    private Seeker _seeker;

    private Rigidbody2D _rb;

    private int _currentWaypoint = 0;

    private bool _reachedEndOfPath = false;
    private bool _isShooting = false;
    private bool _isInShootingRange = false;
    private bool canShoot = true;

    public float _enemySpeed = 200f;
    public float _nextWaypointDistance = 2f;
    public float _shootRange, _shootSpeed;
    public float _timeBeforeNextShot = 1.0f;

    [SerializeField] public float _UpdatePathRate = .5f;
    [SerializeField] public float _distanceVal = 5.0f;
    private void Awake()
    {
        _seeker = GetComponent<Seeker>();
        _rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        InvokeRepeating("UpdatePath", 0f, _UpdatePathRate);
    }

    private void Update()
    {
        animator.SetBool("IsShooting", _isInShootingRange);
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
        float range = 15.0f;
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
            _enemyGFX.localScale = new Vector3(-0.3f, 0.3f, 1f);
        }
        else if (_rb.velocity.x <= -0.0f && force.x < 0f)
        {
            _enemyGFX.localScale = new Vector3(0.3f, 0.3f, 1f);
        }
    }

    IEnumerator Shoot()
    {
        canShoot = false;
        yield return new WaitForSeconds(_timeBeforeNextShot);
        GameObject newBullet = Instantiate(_bulletPrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));

        newBullet.GetComponent<Rigidbody2D>().velocity = (_playerModel.position - transform.position).normalized * _shootSpeed;
        canShoot = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, _shootRange);
    }

}