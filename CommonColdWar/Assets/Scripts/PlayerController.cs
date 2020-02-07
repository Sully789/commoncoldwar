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

    public Rigidbody2D rb;
    public Joystick joystick;
    public Boundary boundary;
    public GameObject shot;
    public Transform shotSpawn;
    public Button shootButton;

    public Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
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

        Shoot();


    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);
    }

    void Shoot()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            // Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            Instantiate(shot, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), gameObject.transform.rotation);
            GetComponent<AudioSource>().Play();
        }
    }
}
