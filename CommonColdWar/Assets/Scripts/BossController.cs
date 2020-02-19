/*
 * Sean O'Sullivan, K00180620, Cross Platform Games Development, CA1
 * BossController.cs handles the Boss health, damage and collisions
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public GameObject explosion;        //Game Object that holds the VFX of the Boss death explosion
    public GameObject playerExplosion;  //Game Object that holds the VFX of the Player death explosion

    public int health;                  //int value that stores the health of the boss, can be changed for each boss
    public int scoreValue;              //int value that stores the score for defeating the boss, can be changed for each boss
    public int damage;                  //int value that stores the Players damage to the boss, can be changed for each boss

    private GameManager gameManager;    //Game Manager used for updating Score

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

    //Function takes health away from the boss
    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    //Function plays particle effect and increments score when boss is killed
    void Die()
    {
        Instantiate(explosion, transform.position, transform.rotation);
        gameManager.AddScore(scoreValue);
        Destroy(gameObject);
    }

    //Trigger colliders
    private void OnTriggerEnter2D(Collider2D other)
    {
        //If Boss collides with Players shot it takes damage
        if (other.CompareTag("Player Shot"))
        {
            TakeDamage(damage);
            Destroy(other.gameObject);
        }

        //If Boss collides with Player, the Player is destroyed
        if (other.CompareTag("Player"))
        {
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            Destroy(other.gameObject);
            gameManager.GameOver();
        }
    }

}
