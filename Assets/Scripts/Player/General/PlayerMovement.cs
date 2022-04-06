using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Animator animator;
    PauseMenu pauseMenu;

    //Properties of the player
    Transform trans;
    Rigidbody2D body;

    //Getting the arm for a visual fix
    public GameObject playerArm;

    //Movement speed and jump force
    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    [SerializeField] float maxSpeed;
    [SerializeField] private LayerMask jumpableGround;

    private BoxCollider2D _coll;

    //Some bools for movement
    float runInput;
    private bool jumpInput;
    private bool isWalking;
    private bool isJumping;
    private bool CanMove = true;

    //Can enter a shop
    private bool canEnterShop = false;
    private bool shopInput = false;

    private float playerGravity = 7f;


    // Start is called before the first frame update
    void Start()
    {
        _coll = GetComponent<BoxCollider2D>();
        trans = GetComponent<Transform>();
        body = GetComponent<Rigidbody2D>();
        playerArm = GameObject.Find("Arm");
    }

    // Update is called once per frame
    void Update()
    {

        runInput = Input.GetAxis("Horizontal");

        if (CanMove)
        {

            if (Input.GetKeyDown(KeyCode.W))
            {
                jumpInput = true;
            }

            if (Input.GetKeyDown(KeyCode.E) && canEnterShop)
            {
                shopInput = true;
            }

            if (Input.GetKeyUp(KeyCode.W))
            {
                jumpInput = false;
                shopInput = false;
            }

            if (runInput == 0 && body.velocity.y == 0)
            {
                isWalking = false;
                body.drag = 10;
            }
            else
            {
                body.drag = 2;
            }
        }

        Debug.Log(runInput);

        animator.SetBool("IsWalking", isWalking);
        animator.SetBool("IsJumping", jumpInput);
    }

    void FixedUpdate()
    {
        if (CanMove)
        {
            if (runInput != 0)
            {
                Walk();
            }
        }

        if (jumpInput && isGrounded())
        {
            Jump();
        }
    }

    void Walk()
    {
        if (Mathf.Abs(body.velocity.x) >= maxSpeed)
        {
            return;
        }

        if (runInput > 0)
        {
            body.AddForce(Vector2.right * speed, ForceMode2D.Force);
            trans.rotation = Quaternion.Euler(0, 0, 0);
            //Rotates the arm to match the player
            playerArm.transform.localScale = new Vector3(1, 1, 1);
            isWalking = true;
        }
        if (runInput < 0)
        {
            body.AddForce(Vector2.left * speed, ForceMode2D.Force);
            trans.rotation = Quaternion.Euler(0, 180, 0);
            //Rotates the arm to match the player
            playerArm.transform.localScale = new Vector3(1, -1, 1);
            isWalking = true;
        }
        if (runInput == 0)
        {
            isWalking = false;
        }
    }

    void Jump()
    {
        body.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        jumpInput = true;
    }

    public float GetSpeed()
    {
        return speed;
    }

    public bool GetShopInput()
    {
        return shopInput;
    }

    private bool isGrounded()
    {
        float extraHeightText = .1f;
        return Physics2D.BoxCast(_coll.bounds.center, _coll.bounds.size, 0f, Vector2.down, extraHeightText, jumpableGround);
    }

    public void SetCanMove(bool move)
    {
        CanMove = move;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Shop"))
        {
            canEnterShop = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Shop"))
        {
            canEnterShop = false;
        }
    }
}
