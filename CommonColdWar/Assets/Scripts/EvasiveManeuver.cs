using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvasiveManeuver : MonoBehaviour
{
    public int evadeType;
    public float dodge;
    public float dodgeX;
    public float dodgeY;
    public float smoothing;
    public float tilt;
    public float xRange = 8; //Range on x axis used to keep player in game area
    public float yRange = 5; //Range on y axis used to keep player in game area

    public Vector2 startWait;
    public Vector2 maneuverTime;
    public Vector2 maneuverWait;
    public Boundary boundary;

    private float currentSpeed;
    private float targetManeuver;
    private float targetManeuverX;
    private float targetManeuverY;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentSpeed = rb.velocity.y;  //or x
        StartCoroutine(Evade());
    }

    IEnumerator Evade()
    {
        yield return new WaitForSeconds(Random.Range(startWait.x, startWait.y));

        while (true)
        {
            targetManeuver = Random.Range(1, dodge) * -Mathf.Sign(transform.position.x);
            //targetManeuverX = Random.Range(1, dodgeX) * -Mathf.Sign(transform.position.x);
           // targetManeuverY = Random.Range(1, dodgeY) * -Mathf.Sign(transform.position.y);
            yield return new WaitForSeconds(Random.Range(maneuverTime.x, maneuverTime.y));
            targetManeuver = 0;
           // targetManeuverX = 0;
           // targetManeuverY = 0;
            yield return new WaitForSeconds(Random.Range(maneuverWait.x, maneuverWait.y));
        }
    }


    void FixedUpdate()
    {
        PerformManeuver(evadeType);
       // KeepInBounds();
        
        rb.position = new Vector3
        (
            Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
            Mathf.Clamp(rb.position.y, boundary.yMin, boundary.yMax),
            0.0f
        );
        

       // rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);  tilt script here
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

    void PerformManeuver(int evadeType)
    {
        float newManeuver;
        switch (evadeType)
        {
            case 1:
                Debug.Log("Case 1");
                newManeuver = Mathf.MoveTowards(rb.velocity.x, targetManeuver, Time.deltaTime * smoothing);
                rb.velocity = new Vector3(newManeuver, 0.0f, currentSpeed);
                break;

            case 2:
                Debug.Log("Case 2");
                newManeuver = Mathf.MoveTowards(targetManeuver, rb.velocity.y, Time.deltaTime * smoothing);
                rb.velocity = new Vector3(0.0f, newManeuver, currentSpeed);
                Debug.Log("Target Maneuver: " + targetManeuverX);
                Debug.Log("New Maneuver" + newManeuver);
               // Debug.Log("Case 2");
                break;

            case 3:
                Debug.Log("Case 3");
                newManeuver = Mathf.MoveTowards(rb.velocity.x, targetManeuverY, Time.deltaTime * smoothing);
                rb.velocity = new Vector3(newManeuver, 0.0f, currentSpeed);
                break;

            case 4:
                newManeuver = Mathf.MoveTowards(rb.velocity.x, targetManeuverY, Time.deltaTime * smoothing);
                rb.velocity = new Vector3(newManeuver, 0.0f, currentSpeed);
                break;

            default:
                newManeuver = Mathf.MoveTowards(rb.velocity.x, targetManeuverY, Time.deltaTime * smoothing);
                rb.velocity = new Vector3(newManeuver, 0.0f, currentSpeed);
                break;
        }
        
    }

}
