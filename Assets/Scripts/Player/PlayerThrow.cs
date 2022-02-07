using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThrow : MonoBehaviour
{

    [SerializeField] GameObject gunPrefab;
    [SerializeField] GameObject cursor;
    [SerializeField] GameObject player;
    [SerializeField] GameObject playerArmEnd;
    [SerializeField] float gunSpeed;


    // Update is called once per frame
    void Update()
    {
        //Arm Rotation
        Vector3 angle = cursor.transform.position - player.transform.position;
        float rotated = Mathf.Atan2(angle.y, angle.x) * Mathf.Rad2Deg;
        player.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotated);

        //Click to shoot
        if (Input.GetButtonDown("Fire1"))
        {
            float distance = angle.magnitude;
            Vector2 direction = angle / distance;
            direction.Normalize();
            Shoot(direction, rotated);
        }

    }

    void Shoot(Vector2 direction, float rotated)
    {
        //Throwing Gun
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var bullet = Instantiate(gunPrefab) as GameObject;
        bullet.transform.position = playerArmEnd.transform.position;
        bullet.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotated);
        bullet.GetComponent<Rigidbody2D>().velocity = direction * gunSpeed;
        Destroy(bullet, 5);
    }
}
