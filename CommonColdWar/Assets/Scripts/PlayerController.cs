using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    // public float horizontalMove;
    //public float verticalMove;
    private float nextFire;
    public float fireRate;
    public float xRange = 8; //Range on x axis used to keep player in game area
    public float yRange = 5; //Range on y axis used to keep player in game area

    public Rigidbody2D rb;
    public Joystick joystick;
    public Boundary boundary;
    public GameObject shot;
    public GameObject shield;
    public Transform[] shotSpawns;
    public Button shootButton;

    public Vector2 movement;

    private GameManager gameManager;

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

       // Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        // horizontalMove = joystick.Horizontal * moveSpeed;
     movement.x = joystick.Horizontal * moveSpeed;
        // verticalMove = joystick.Vertical * moveSpeed;
        movement.y = joystick.Vertical * moveSpeed;

        if (joystick.Horizontal >= .2f)
        {
            // horizontalMove = moveSpeed;
            movement.x = moveSpeed;
        }
        else if(joystick.Horizontal <= -.2f)
        {
            // horizontalMove = -moveSpeed;
            movement.x = -moveSpeed;
        }
        else
        {
            // horizontalMove = 0;
            movement.x = 0;
        }

        if (joystick.Vertical >= .2f)
        {
            //verticalMove = moveSpeed;
            movement.y = moveSpeed;
        }
        else if (joystick.Vertical <= -.2f)
        {
            //verticalMove = -moveSpeed;
            movement.y = -moveSpeed;
        }
        else
        {
            //verticalMove = 0;
            movement.y = 0;
        }

        // for(int i = 0; i < Input.touchCount; i++)
        // {
        //     Vector3 touchPositon = Camera.main.ScreenToViewportPoint(Input.touches[i].position);
        //    Debug.DrawLine(Vector3.zero, touchPositon, Color.blue);
        //  }

        //Shoot();
        KeepInBounds();
        PowerUpVFX();

    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);

    }

    void KeepInBounds() //Method ensures player does not move out of the game area
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
                    //Instantiate(shot, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), gameObject.transform.rotation);
                }
            }
            else
            {
                Instantiate(shot, shotSpawns[0].position, shotSpawns[0].rotation);
                //Instantiate(shot, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), gameObject.transform.rotation);
            }
            
            // Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            
            GetComponent<AudioSource>().Play();
        }

         
    }

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

    private void OnTriggerEnter2D(Collider2D other)
    {

        if(other.CompareTag("Shield Powerup"))
        {
            gameManager.setShieldActive(true);
            Destroy(other.gameObject);
        }
        //Prob wont need this? already true
       // if(other.CompareTag("Shield Powerup") && gameManager.getShieldActive() == true)
      //  {
      //      Destroy(other.gameObject);
      //  }

        if (other.CompareTag("Shot Powerup"))
        {
            gameManager.setShotActive(true);
            Destroy(other.gameObject);
            StartCoroutine(ShotPowerupCountdownRoutine());
        }


        // if()
        // if (other.CompareTag("Boundary") || other.CompareTag("Enemy"))
        // {
        //      return;
        //   }

        /*
           if (gameManager.getShieldActive() == true)
           {
               Debug.Log("Shield already On");
               //turn on VFX
           }

           if (gameManager.getShieldActive() == false)
           {

               //turn on VFX
               // StartCoroutine(PowerupCountdownRoutine());  For shot, shield stays on till hit
           }

           */
    }

    IEnumerator ShotPowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(20);
        gameManager.setShotActive(false);
    }

}
