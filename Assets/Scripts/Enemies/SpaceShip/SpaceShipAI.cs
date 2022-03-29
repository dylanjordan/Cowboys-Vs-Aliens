using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class SpaceShipAI : MonoBehaviour
{
    public Transform _player;

    public Transform _enemyGFX;

    private Path _path;

    private Seeker _seeker;

    private Rigidbody2D _rb;

    private int _currentWaypoint = 0;

    private bool _reachedEndOfPath = false;

    public float _enemySpeed = 200f;
    public float _nextWaypointDistance = 2f;
    [SerializeField] public float _UpdatePathRate = .5f;
    private void Awake()
    {
        _seeker = GetComponent<Seeker>();
        _rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        InvokeRepeating("UpdatePath", 0f, _UpdatePathRate);
    }

    private void UpdatePath()
    {
        if (_seeker.IsDone())
        {
            _seeker.StartPath(_rb.position, _player.position, OnPathComplete);
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
        FindPath();

        UpdateRotation();
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
    }
    private void UpdateRotation()
    {
        if (_rb.velocity.x >= 0.01f)
        {
            _enemyGFX.localScale = new Vector3(-0.25f, 0.25f, 1f);
        }
        else if (_rb.velocity.x <= -0.0f)
        {
            _enemyGFX.localScale = new Vector3(0.25f, 0.25f, 1f);
        }
    }
}
