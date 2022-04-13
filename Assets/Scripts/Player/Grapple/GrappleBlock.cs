using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleBlock : MonoBehaviour
{

    private SpriteRenderer block;

    public Color selected, colorBase;

    private SpringJoint2D playerSpring;

    public GameObject player;

    public LineRenderer _lineRenderer;

    public AudioClip grappleNoise;

    public void Start()
    {
        playerSpring = GameObject.FindGameObjectWithTag("Player").GetComponent<SpringJoint2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        block = GetComponent<SpriteRenderer>();
        _lineRenderer.enabled = false;
        playerSpring.enabled = false;
    }

    private void Update()
    {
        if (playerSpring.enabled)
        {
            _lineRenderer.SetPosition(0, player.transform.position);
            _lineRenderer.SetPosition(1, gameObject.transform.position);
            if (Input.GetMouseButtonUp(1))
            {
                playerSpring.enabled = false;
                _lineRenderer.enabled = false;
            }
        }
        if (!playerSpring.enabled)
        {
            _lineRenderer.enabled = false;
        }
    }

    public void OnMouseOver()
    {
        block.color = selected;
        if (Input.GetMouseButtonDown(1))
        {
            playerSpring.enabled = true;
            _lineRenderer.enabled = true;
            playerSpring.distance = Vector2.Distance(gameObject.transform.position, player.transform.position);
            playerSpring.connectedBody = GetComponent<Rigidbody2D>();
            AudioSource.PlayClipAtPoint(grappleNoise, transform.position);
        }
        if (Input.GetMouseButtonUp(1))
        {
            _lineRenderer.enabled = false;
            playerSpring.enabled = false;
        }
        if (playerSpring.distance >= 5)
        {
            _lineRenderer.enabled = false;
            playerSpring.enabled = false;
        }
    }

    public void OnMouseExit()
    {
        block.color = colorBase;
    }

}
