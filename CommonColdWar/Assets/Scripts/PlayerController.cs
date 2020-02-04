using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float horizontalMove;
    public float verticalMove;

    public Joystick joystick;
    public Boundary boundary;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = joystick.Horizontal * moveSpeed;
        verticalMove = joystick.Vertical * moveSpeed;

        if (joystick.Horizontal >= .2f)
        {
            horizontalMove = moveSpeed;
        }
        else if(joystick.Horizontal <= -.2f)
        {
            horizontalMove = -moveSpeed;
        }
        else
        {
            horizontalMove = 0;
        }

        if (joystick.Vertical >= .2f)
        {
            verticalMove = moveSpeed;
        }
        else if (joystick.Vertical <= -.2f)
        {
            verticalMove = -moveSpeed;
        }
        else
        {
            verticalMove = 0;
        }

        // for(int i = 0; i < Input.touchCount; i++)
        // {
        //     Vector3 touchPositon = Camera.main.ScreenToViewportPoint(Input.touches[i].position);
        //    Debug.DrawLine(Vector3.zero, touchPositon, Color.blue);
        //  }
    }
}
