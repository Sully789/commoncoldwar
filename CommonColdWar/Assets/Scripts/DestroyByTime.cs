/*
 * Sean O'Sullivan, K00180620, Cross Platform Games Development, CA1
 * DestroyByTime.cs removes objects from the scene to free up resources
 * From Unity's Space Shooter tutorial: https://learn.unity.com/project/space-shooter-tutorial
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByTime : MonoBehaviour
{
    public float lifetime;      //float variable to determine the time that object stays in scene, can be changed for each object

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifetime);
    }
}
