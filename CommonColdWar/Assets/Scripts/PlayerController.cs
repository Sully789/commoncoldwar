/*
 * Sean O'Sullivan, K00180620, Cross Platform Games Development, CA1
 * PlayerController.cs handles Player Input, Keeps the Player Inbounds, and handles Power Up VFX
 * Joystick implementation apdated from: https://www.youtube.com/watch?v=bp2PiFC9sSs
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;             //float value holds the movement speed
    public float fireRate;              //float value holds the rate of fire

    public Rigidbody2D rb;              //Rigidbody2D of the Player
    public Joystick joystick;           //Joystick used for mobile touch controls
    public GameObject shot;             //GameObject that holds the Players shot
    public GameObject shield;           //GameObject that holds the VFX of the Players shield
    public Transform[] shotSpawns;      //Transform Array that holds the positions where the Players shot comes from
    public Button shootButton;          //UI that holds the Shoot button

    public Vector2 movement;            //Vector2 used to move

    private GameManager gameManager;    //Game Manager used for Power Up logic
    private float nextFire;             //float to hold the time when Player can shoot again
    private float shotPowerUpLength=20; //float to hold the amount of time the Shot Power Up is active
    private float xRange = 8;           //Range on x axis used to keep Player in game area
    private float yRange = 5;           //Range on y axis used to keep Player in game area

    // Start is called before the first frame update
    void Start()
    {
        shield.GetComponent<GameObject>();
        GameObject gameManagerObject = GameObject.FindWithTag("GameController");
        if (gameManagerObject != null)
        {
            gameManager = gameManagerObject.GetComponent<GameManager>();
        }
        if (gameManager == null)
        {
            Debug.Log("Cannot find 'GameManager' script");
        }
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = joystick.Horizontal * moveSpeed;
        movement.y = joystick.Vertical * moveSpeed;

        if (joystick.Horizontal >= .2f)
        {
            movement.x = moveSpeed;
        }
        else if(joystick.Horizontal <= -.2f)
        {
            movement.x = -moveSpeed;
        }
        else
        {
            movement.x = 0;
        }

        if (joystick.Vertical >= .2f)
        {
            movement.y = moveSpeed;
        }
        else if (joystick.Vertical <= -.2f)
        {
            movement.y = -moveSpeed;
        }
        else
        {
            movement.y = 0;
        }

        KeepInBounds();
        PowerUpVFX();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);

    }

    //Ensures Player does not move out of the game area
    void KeepInBounds()
    {
        if (transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }
        else if (transform.position.y < -yRange)
        {
            transform.position = new Vector3(transform.position.x, -yRange, transform.position.z);
        }
        else if (transform.position.y > xRange)
        {
            transform.position = new Vector3(transform.position.x, yRange, transform.position.z);
        }
    }

    //Shoots when Player pressed the Fire Button
    public void Shoot()
    {
        
        //if (Input.GetButton("Fire1") && Time.time > nextFire) // Fix this to shoot when button is pressed
        if (Time.time > nextFire) // Fix this to shoot when button is held
        {
            nextFire = Time.time + fireRate;
            if (gameManager.getShotActive() == true)
            {
                foreach (var shotSpawn in shotSpawns)
                {
                    Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
                }
            }
            else
            {
                Instantiate(shot, shotSpawns[0].position, shotSpawns[0].rotation);
            }
            
            GetComponent<AudioSource>().Play();
        }

         
    }

    //Turns on and off the Shield VFX when Power Up is picked up
    public void PowerUpVFX()
    {
        if(gameManager.getShieldActive() == true)
        {
            shield.gameObject.SetActive(true);
        }
        else
        {
            shield.gameObject.SetActive(false);
        }
    }

    //Trigger colliders to handle Power Ups
    private void OnTriggerEnter2D(Collider2D other)
    {

        if(other.CompareTag("Shield Powerup"))
        {
            gameManager.setShieldActive(true);
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Shot Powerup"))
        {
            gameManager.setShotActive(true);
            Destroy(other.gameObject);
            StartCoroutine(ShotPowerupCountdownRoutine());
        }

    }

    //Coroutine that handles the length of the Shot Power Up
    IEnumerator ShotPowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(shotPowerUpLength);
        gameManager.setShotActive(false);
    }

}
