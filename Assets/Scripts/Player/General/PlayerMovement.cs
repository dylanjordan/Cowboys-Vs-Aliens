using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Animator animator;
    PauseMenu pauseMenu;
    //Properties of the player
    Transform trans;
    public Rigidbody2D body;

    //Getting the arm for a visual fix
    public GameObject playerArm;

    //Movement speed and jump force
    public float speed;
    [SerializeField] float jumpForce;
    [SerializeField] private LayerMask jumpableGround;

    private BoxCollider2D _coll;

    //Some bools for movement
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
        if (CanMove)
        {
            Walk();

            if (Input.GetKeyDown(KeyCode.W) && !canEnterShop)
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
        }

        animator.SetBool("IsWalking", isWalking);
        animator.SetBool("IsJumping", jumpInput);

    }

    void FixedUpdate()
    {
        if (jumpInput && isGrounded())
        {
            Jump();
        }
    }

    void Walk()
    {
        if (Input.GetKey(KeyCode.A))
        {
            trans.position += transform.right * Time.deltaTime * speed;
            trans.rotation = Quaternion.Euler(0, 180, 0);
            //Rotates the arm to match the player
            playerArm.transform.localScale = new Vector3(1, -1, 1);
            isWalking = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            trans.position += transform.right * Time.deltaTime * speed;
            trans.rotation = Quaternion.Euler(0, 0, 0);
            playerArm.transform.localScale = new Vector3(1, 1, 1);
            isWalking = true;
        }
        if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
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
