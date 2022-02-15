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

    //How long before recall?
    public float throwTime;

    // Start is called before the first frame update
    void Start()
    {
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
            Debug.Log(_playerPos.transform.position);
            //Get the direction the player has to move to get to the player
            Vector3 dirToPlayer = (_playerPos.transform.position - transform.position).normalized;

            //Go to player
            _rb.velocity = dirToPlayer * 20f;

        }

        //If 2 seconds pass, recall gun
        if (throwTime > 10f)
        {
            Debug.Log("Come Here!");
            recalling = true;
            throwTime = 0;
        }
        throwTime += 0.1f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //If the gun collides with something, start recalling
        if (collision.collider.tag == "Target" || collision.collider.tag == "Ground")
        {
            Debug.Log("Come Here!");
            _rb.velocity = new Vector2(0f, 0f);
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
