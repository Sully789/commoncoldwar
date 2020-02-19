/*
 * Sean O'Sullivan, K00180620, Cross Platform Games Development, CA1
 * RandomRotator.cs rotates the object with a random values
 * From Unity's Space Shooter tutorial: https://learn.unity.com/project/space-shooter-tutorial
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotator : MonoBehaviour
{
    public float minTumble;     //Minimum range of the objects rotation
    public float maxTumble;     //Maximum range of the objects rotation
    public Rigidbody2D rb;      //Rigidbody2D of the object

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.angularVelocity = Random.Range(minTumble, maxTumble);
    }
}
