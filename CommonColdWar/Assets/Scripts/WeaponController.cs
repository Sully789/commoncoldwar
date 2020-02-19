/*
 * Sean O'Sullivan, K00180620, Cross Platform Games Development, CA1
 * WeaponContoller.cs handles Shot, Missile and Laser attacks by Enemy and Boss, different attacks can be assigned for each object
 * Adpated From Unity's Space Shooter tutorial: https://learn.unity.com/project/space-shooter-tutorial
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject shot;     //GameObject that holds the object to be shot
    public GameObject missle;   //GameObject that holds missile to be shot
    public GameObject laser;    //GameObject that holds laser to be shot
    public Transform shotSpawn; //Transform that holds the position of where the shot is instantiated
    public float fireRate;      //float that holds the rate of fire
    public float delay;         //float that holds value to delay each shot
    public float missleDelay;   //float that holds value to delay modifier of missile
    public float laserDelay;    //float that holds value to delay modifier of laser

    private AudioSource audioSource;    //AudioSource that holds the shot SFX

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (shot != null)
        {
            InvokeRepeating("Fire", delay, fireRate);
        }
        if(missle != null)
        {
            InvokeRepeating("FireMissile", delay + missleDelay, fireRate);
        }
        if(laser != null)
        {
            InvokeRepeating("FireLaser", delay + laserDelay, fireRate);
        }
        
    }

    //Shoots basic Shot
    void Fire()
    {
        Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        audioSource.Play();
    }

    //Shoots Missile
    void FireMissile()
    {
        Instantiate(missle, shotSpawn.position, shotSpawn.rotation);
        audioSource.Play();
    }

    //Shoots Laser
    void FireLaser()
    {
        Instantiate(laser, shotSpawn.position, shotSpawn.rotation);
        audioSource.Play();
    }
}
