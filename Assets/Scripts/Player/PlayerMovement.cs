using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Properties of the player
    Transform trans;
    public Rigidbody2D body;

    //Movement speed and jump force
    [SerializeField] float speed;
    [SerializeField] float jumpForce;

    //Some bools for movement
    private bool jumpInput;
    private bool isGrounded;
    private bool isWalking;

    private float playerGravity = 7f;

    // Start is called before the first frame update
    void Start()
    {
        trans = GetComponent<Transform>();
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Walk();

        if (Input.GetKeyDown(KeyCode.W))
        {
            jumpInput = true;
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            jumpInput = false;
        }
    }

    void FixedUpdate()
    {
        if (jumpInput && isGrounded)
        {
            Jump();
        }
    }

    void Walk()
    {
        if (Input.GetKey(KeyCode.A))
        {
            trans.position -= transform.right * Time.deltaTime * speed;
            trans.rotation = Quaternion.Euler(0, 0, 0);
            isWalking = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            trans.position -= transform.right * Time.deltaTime * speed;
            trans.rotation = Quaternion.Euler(0, 180, 0);
            isWalking = true;
        }
    }

    void Jump()
    {
        body.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        isGrounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            for (int i = 0; i < collision.contacts.Length; i++)
            {
                if (collision.contacts[i].normal.y > 0.5)
                {
                    isGrounded = true;
                }
            }
        }
    }
}
