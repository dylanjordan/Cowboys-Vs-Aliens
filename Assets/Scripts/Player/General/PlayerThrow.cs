using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThrow : MonoBehaviour
{
    PauseMenu pauseMenu;

    [SerializeField] GameObject gunPrefab;
    [SerializeField] GameObject cursor;
    [SerializeField] GameObject player;
    [SerializeField] GameObject playerArmEnd;
    [SerializeField] GameObject fakeGun;

    public float gunSpeed;
    
    public bool canThrow = true;

    void Update()
    {
        if (!PauseMenu.isPaused)
        {
            Vector3 angle = cursor.transform.position - player.transform.position;
            float rotated = Mathf.Atan2(angle.y, angle.x) * Mathf.Rad2Deg;
            player.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotated);

            if (Input.GetMouseButtonDown(0) && canThrow)
            {
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
        Destroy(bullet, 7);
    }
}
