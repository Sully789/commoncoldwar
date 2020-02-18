using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    public GameObject explosion;
    public GameObject playerExplosion;

    public int scoreValue;

    private GameManager gameManager;

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


    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("collide (name) : " + other.gameObject.name);
        Debug.Log("collide (tag) : " + other.gameObject.tag);
        if (other.CompareTag("Boundary") || other.CompareTag("Enemy") || other.CompareTag("Shield Powerup") || other.CompareTag("Shot Powerup") || other.CompareTag("Boss"))
        {
            return;
        }

        if (explosion != null)
        {
            Instantiate(explosion, transform.position, transform.rotation);
        }

        if (other.CompareTag("Player") && gameManager.getShieldActive() == true)
        {
            gameManager.setShieldActive(false);
            Destroy(gameObject);
            return;
            // 
            //Play sound effect to deystroy shield
        }
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
