using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpEnemy : MonoBehaviour
{
    public Animator animator;

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
    private bool _isJumping = false;

    [Header("For Jump Attack")]
    [SerializeField] float _jumpheight;
    [SerializeField] Transform _player;
    [SerializeField] Transform AttackGroundCheck;
    [SerializeField] Vector2 boxSize;
    [SerializeField] float _attackRate;
    public AudioClip jumpNoise;

    private bool _isGrounded;
    private bool _canAttack = true;

    [Header("For Seeing Player")]
    [SerializeField] Vector2 lineOfSite;
    [SerializeField] LayerMask playerLayer;
    private bool _canSeePlayer;
    [Header("Other")]
    private Rigidbody2D _rb;

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
        UpdateJumper();

        animator.SetBool("IsJumping", _isJumping);
    }
    private void FixedUpdate()
    {
        checkingGround = Physics2D.OverlapCircle(groundCheck.position, _circleRadius, groundLayer);
        checkingWall = Physics2D.OverlapCircle(wallCheck.position, _circleRadius, groundLayer);
        _isGrounded = Physics2D.OverlapBox(AttackGroundCheck.position, boxSize, 0, groundLayer);
        _canSeePlayer = Physics2D.OverlapBox(transform.position, lineOfSite, 0, playerLayer);
        
        if (!_canSeePlayer && _isGrounded)
        {
            Patrolling();
        }
        if (_canSeePlayer && _isGrounded && _canAttack)
        {
            StartCoroutine(AttackRate());
        }
        if (_canSeePlayer)
        {
            FlipTowardsPlayer();
        }

        if (!_isGrounded)
        {
            _isJumping = true;
        }
        else
        {
            _isJumping = false;
        }
    }

    public void TakeDamage()
    {
        _enemyHealth -= _damageRecieved;
    }

    private void UpdateJumper()
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
    private IEnumerator AttackRate()
    {
        JumpAttack();
        _canAttack = false;
        yield return new WaitForSeconds(_attackRate);
        _canAttack = true;
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
        _rb.velocity = new Vector2(_enemySpeed * _moveDir, _rb.velocity.y);
    }

    void JumpAttack()
    {
        float distFromPlayer = _player.position.x - transform.position.x;

        if (_isGrounded)
        {
            _rb.AddForce(new Vector2(distFromPlayer, _jumpheight), ForceMode2D.Impulse);
            AudioSource.PlayClipAtPoint(jumpNoise, transform.position);
        }
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

        Gizmos.color = Color.blue;
        Gizmos.DrawCube(AttackGroundCheck.position, boxSize);

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
