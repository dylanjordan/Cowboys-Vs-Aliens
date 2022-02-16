using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThrow : MonoBehaviour
{

    //Gun Prefab
    [SerializeField] GameObject gunPrefab;

    //Cursor
    [SerializeField] GameObject cursor;

    //Player location and firing location
    [SerializeField] GameObject player;
    [SerializeField] GameObject playerArmEnd;

    //Decorative handgun
    [SerializeField] GameObject fakeGun;

    //Player's firing speed
    [SerializeField] float gunSpeed;
    
    //Tracks if the player can fire
    public bool canThrow = true;

    // Update is called once per frame
    void Update()
    {
        //Arm Rotation
        Vector3 angle = cursor.transform.position - player.transform.position;
        float rotated = Mathf.Atan2(angle.y, angle.x) * Mathf.Rad2Deg;
        player.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotated);

        //Click to shoot
        if (Input.GetMouseButtonDown(0) && canThrow)
        {
            //Get the direction and angle for the gun and Throw eeet
            float distance = angle.magnitude;
            Vector2 direction = angle / distance;
            direction.Normalize();
            Shoot(direction, rotated);
            canThrow = false;
        }

        //If the gun prefab exists in the gameworld
        if (GameObject.Find("Gun(Clone)") != null)
        {
            //Get rid of the gun in the players hand 
            fakeGun.SetActive(false);
            //Dont let the player throw another
            canThrow = false;
        }
        //If the gun prefab doesnt exist in the gameworld
        else if (GameObject.Find("Gun(Clone)") == null)
        {
            //Make the gun in the players hand appear
            fakeGun.SetActive(true);
            //Let the player throw one
            canThrow = true;
        }
    }

    //Shoots weapon
    void Shoot(Vector2 direction, float rotated)
    {
        //Throwing Gun
        //Find the position of the mouse
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var bullet = Instantiate(gunPrefab) as GameObject;
        //Set the prefab's position to the end of the players arm
        bullet.transform.position = playerArmEnd.transform.position;
        //Rotate the prefab to match the rotation of the arm
        bullet.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotated);
        //Shoot it in the direction of the cursor
        bullet.GetComponent<Rigidbody2D>().velocity = direction * gunSpeed;
        //If it doesnt return to the player naturally (it gets blocked), destroy it
        Destroy(bullet, 10);
    }
}
