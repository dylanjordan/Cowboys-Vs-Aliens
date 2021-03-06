using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    //Gun and Player
    GameObject _playerPos;

    [SerializeField] GameObject _gunPos;

    //Gun rigidbody
    public Rigidbody2D _rb;

    //Collision Audio
    public AudioClip _attackSound;

    //Is the gun recalling
    bool recalling = false;

    //Recall Timer
    float throwTime;

    // Start is called before the first frame update
    void Start()
    {
        //Standard Biz
        //Find the player in the game and assign it to this variable
        _playerPos = GameObject.Find("Player");
        _gunPos.GetComponent<GameObject>();
        _rb.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //If the gun is recalling, move to player
        if (recalling)
        {
            //Get the direction the player has to move to get to the player
            Vector3 dirToPlayer = (_playerPos.transform.position - transform.position).normalized;

            //Go to player
            _rb.velocity = dirToPlayer * 30f;
        }

        //If enough time passes, recall gun
        if (throwTime > 5f)
        {
            Debug.Log("Come Here!");
            recalling = true;
            //Reset Time
            throwTime = 0;
        }
        //Timer
        throwTime += 0.1f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //If the gun collides with something, start recalling
        if (collision.collider.tag != "Player")
        {
            Debug.Log("Come Here!");
            AudioSource.PlayClipAtPoint(_attackSound, transform.position);
            recalling = true;
        }
        //If the gun collides with the player, Disappear
        if (collision.collider.tag == "Player")
        {
            if (recalling)
            {
                Debug.Log("I've returned!");
                recalling = false;
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            if (recalling)
            {
                Debug.Log("I've returned!");
                recalling = false;
                Destroy(gameObject);
            }
        }
    }
}
