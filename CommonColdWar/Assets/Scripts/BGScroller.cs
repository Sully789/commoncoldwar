/*
 * Sean O'Sullivan, K00180620, Cross Platform Games Development, CA1
 * BGScroller.cs scrolls the background
 * From Unity's Space Shooter tutorial: https://learn.unity.com/project/space-shooter-tutorial
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroller : MonoBehaviour
{
    private float scrollSpeed = 0.25f;  //float variable to determine the speed at which the background scrolls
    private float tileSizeX = 17f;      //float variable used to repeat the background

    private Vector3 startPosition;      //Vector3 that holds the inital position of the background

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSizeX);
        transform.position = startPosition + Vector3.left * newPosition;
    }
}
