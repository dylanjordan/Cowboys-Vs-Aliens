using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleBlock : MonoBehaviour
{

    private SpriteRenderer block;

    public Color selected, colorBase;

    private SpringJoint2D playerSpring;

    public GameObject player;

    public void Start()
    {
        playerSpring = GameObject.FindGameObjectWithTag("Player").GetComponent<SpringJoint2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        block = GetComponent<SpriteRenderer>();
        playerSpring.enabled = false;
    }

    private void Update()
    {
        if (playerSpring.enabled)
        {
            if (Input.GetMouseButtonUp(1))
            {
                playerSpring.enabled = false;
            }
        }
    }

    public void OnMouseOver()
    {
        block.color = selected;
        if (Input.GetMouseButtonDown(1))
        {
            playerSpring.enabled = true;
            playerSpring.distance = Vector2.Distance(gameObject.transform.position, player.transform.position);
            playerSpring.connectedBody = GetComponent<Rigidbody2D>();
        }
        if (Input.GetMouseButtonUp(1))
        {
            playerSpring.enabled = false;
        }
        if (playerSpring.distance >= 5)
        {
            playerSpring.enabled = false;
        }
    }

    public void OnMouseExit()
    {
        block.color = colorBase;
    }

}
