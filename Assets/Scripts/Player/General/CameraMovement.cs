using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] GameObject playerObj;

    Transform trans;
    PlayerMovement player;

    // Start is called before the first frame update
    void Start()
    {
        trans = GetComponent<Transform>();

        player = playerObj.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerObj.transform.position.x - trans.position.x > 5)
        {
            trans.position += transform.right * Time.deltaTime * player.GetSpeed();
        }

        if (playerObj.transform.position.x - trans.position.x < -5)
        {
            trans.position -= transform.right * Time.deltaTime * player.GetSpeed();
        }

        if (playerObj.transform.position.y - trans.position.y > 2)
        {
            trans.position += transform.up * Time.deltaTime * player.GetSpeed();
        }

        if (playerObj.transform.position.y - trans.position.y < -2)
        {
            trans.position -= transform.up * Time.deltaTime * player.GetSpeed();
        }
    }
}
