/*
 * Sean O'Sullivan, K00180620, Cross Platform Games Development, CA1
 * DestroyByContact.cs handles Power Up logic and most Collisions
 * Adapted from Unity's Space Shooter tutorial: https://learn.unity.com/project/space-shooter-tutorial
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    public GameObject explosion;        //Game Object that holds the VFX of the objects death explosion
    public GameObject playerExplosion;  //Game Object that holds the VFX of the Players death explosion

    public int scoreValue;              //int value that stores the score for defeating the object, can be changed for each boss

    private GameManager gameManager;    //Game Manager used for updating Score and for reading PowerUp status

    // Start is called before the first frame update
    void Start()
    {
        GameObject gameManagerObject = GameObject.FindWithTag("GameController");
        if (gameManagerObject != null)
        {
            gameManager = gameManagerObject.GetComponent<GameManager>();
        }
        if (gameManager == null)
        {
            Debug.Log("Cannot find 'GameManager' script");
        }
    }

    //Trigger Colliders
    void OnTriggerEnter2D(Collider2D other)
    {
        //This statement ensures Enemies cannot destroy each other
        if ( other.CompareTag("Enemy") || other.CompareTag("Shield Powerup") || other.CompareTag("Shot Powerup") || other.CompareTag("Boss"))
        {
            return;
        }

        if (explosion != null)
        {
            Instantiate(explosion, transform.position, transform.rotation);
        }
        //If Player is hit and has a Shield Power Up, remove Shield Power Up
        if (other.CompareTag("Player") && gameManager.getShieldActive() == true)
        {
            gameManager.setShieldActive(false);
            Destroy(gameObject);
            return;
        }
        //Else If Player is hit and does not have Shield Power Up, Player is killed
        else if (other.CompareTag("Player") && gameManager.getShieldActive() == false)
        {
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            gameManager.GameOver();
        }
         
        gameManager.AddScore(scoreValue);
        Destroy(other.gameObject);
        Destroy(gameObject);
    }
}
