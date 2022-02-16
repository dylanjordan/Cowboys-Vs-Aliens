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

    //Is the gun recalling
    bool recalling = false;

    //Recall Timer
    float throwTime;

    // Start is called before the first frame update
    void Start()
    {
        //Standard Biz
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
            _rb.velocity = dirToPlayer * 20f;
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
        if (collision.collider.tag == "Target" || collision.collider.tag == "Ground")
        {
            Debug.Log("Come Here!");
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
}
