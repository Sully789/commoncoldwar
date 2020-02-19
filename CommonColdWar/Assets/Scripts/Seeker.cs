/*
 * Sean O'Sullivan, K00180620, Cross Platform Games Development, CA1
 * Seeker.cs moves the object towards the players position
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seeker : MonoBehaviour
{
    public float speed;         //float variable to hold the speed that the object seeks out the player
    private GameObject player;  //Reference to the Player

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //Moves towards Player transform
        if (player != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
        else
        {
            Debug.Log("Cannot find Player");
        }
    }
}
