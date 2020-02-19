/*
 * Sean O'Sullivan, K00180620, Cross Platform Games Development, CA1
 * Mover.cs moves objects across the screen
 * From Unity's Space Shooter tutorial: https://learn.unity.com/project/space-shooter-tutorial
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public float speed;         //float value to determine the speed object moves
    public Rigidbody2D rb;      //Rigidbody2d of object

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.right * speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
