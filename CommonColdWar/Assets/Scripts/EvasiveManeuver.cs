/*
 * Sean O'Sullivan, K00180620, Cross Platform Games Development, CA1
 * EvasiveManeuver.cs moves Enemy and Boss objects through some evasive maneuver, the manuever type can be set in the Inspector
 * Adapted From Unity's Space Shooter tutorial: https://learn.unity.com/project/space-shooter-tutorial
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvasiveManeuver : MonoBehaviour
{
    public int evadeType;           //int value to determine the type of manuever that the Enemy or Boss takes, can be changed for each object
    public float dodgeX;            //Range on x axis used in random function to determine dodge length, can be changed for each object
    public float dodgeY;            //Range on y axis used in random function to determine dodge length, can be changed for each object
    public float smoothing;         //float value used to smooth movement, can be changed for each object
    public float tilt;              //float value used for animation, unused

    public Vector2 startWait;       //Vector2 to used in Coroutine for Random range starting time
    public Vector2 maneuverTime;    //Vector2 to used in Coroutine for Random range movement time
    public Vector2 maneuverWait;    //Vector2 to used in Coroutine for Random range time to wait before making movement

    private float xRange = 8;       //Range on x axis used to keep Enemy or Boss in game area
    private float yRange = 5;       //Range on y axis used to keep Enemy or Boss in game area
    private float currentSpeed;     //float value that takes current speed of the object
    private float targetManeuverX;  //float value of the desired movement on the x axis
    private float targetManeuverY;  //float value of the desired movement on the y axis

    private Rigidbody2D rb;         //Rigidbody2d of the object used for movement of the object

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentSpeed = rb.velocity.x;
        StartCoroutine(Evade());
    }

    //Coroutine used to maneuver the object
    IEnumerator Evade()
    {
        yield return new WaitForSeconds(Random.Range(startWait.x, startWait.y));

        while (true)
        {
            targetManeuverX = Random.Range(1, dodgeX) * -Mathf.Sign(transform.position.x);
            targetManeuverY = Random.Range(1, dodgeY) * -Mathf.Sign(transform.position.y);
            yield return new WaitForSeconds(Random.Range(maneuverTime.x, maneuverTime.y));
            targetManeuverX = 0;
            targetManeuverY = 0;
            yield return new WaitForSeconds(Random.Range(maneuverWait.x, maneuverWait.y));
        }
    }

    //FixedUpdate used for physics calculations
    void FixedUpdate()
    {
        PerformManeuver(evadeType);
        KeepInBounds();
        //rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);  tilt script here
    }

    //This method keeps the object inside the game area
    void KeepInBounds()
    {
        if (transform.position.x < -xRange)
        {
            Destroy(gameObject); // Destroy if leave edge of screen and cant be shot by Player
        }
        else if (transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }
        else if (transform.position.y < -yRange)
        {
            transform.position = new Vector3(transform.position.x, -yRange, transform.position.z);
        }
        else if (transform.position.y > yRange)
        {
            transform.position = new Vector3(transform.position.x, yRange, transform.position.z);
        }
    }

    //Method takes in a int value and performs the different movements inside the switch statement, can be changed for each object
    void PerformManeuver(int evadeType)
    {
        float newManeuver;
        switch (evadeType)
        {
            case 1:
                newManeuver = Mathf.MoveTowards(rb.velocity.x, targetManeuverX, Time.deltaTime * smoothing);
                rb.velocity = new Vector2(newManeuver, 0.0f);
                break;

            case 2:
                newManeuver = Mathf.MoveTowards(rb.velocity.y, targetManeuverY, Time.deltaTime * smoothing);
                rb.velocity = new Vector2(currentSpeed, newManeuver);
                break;

            case 3:
                newManeuver = Mathf.MoveTowards(rb.velocity.x, targetManeuverX, Time.deltaTime * smoothing);
                rb.velocity = new Vector2(newManeuver, newManeuver);
                break;

            default:
                Debug.Log("No Evade Type Found");
                break;
        }        
    }
}
