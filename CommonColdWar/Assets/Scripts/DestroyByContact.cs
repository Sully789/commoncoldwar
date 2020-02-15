using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    public GameObject explosion;
    public GameObject playerExplosion;
    public GameObject shield;

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
        if (other.CompareTag("Boundary") || other.CompareTag("Enemy"))
        {
            return;
        }

        if (other.CompareTag("Shield PowerUp"))
        {
            gameManager.ShieldActive(true);
            shield.SetActive(true);
            //set Shield Active to True
            //Set Ineumertor to for how long
        }

        if (other.CompareTag("Shot PowerUp"))
        {
            gameManager.ShotActive(true);
            //turn on port and starboard shot spawns, might not need this
            //shoot shots from these shot spawns
            //set Shoot Power up Active to True
            //Set Shot Spawn Port and Starboard to active
            //Set Ineumertor to for how long
        }

        if (explosion != null)
        {
            Instantiate(explosion, transform.position, transform.rotation);
        }

        if (other.CompareTag("Player")) //And shield is not active,,, also if shield is active set shield to not active
        {
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            gameManager.GameOver();
        }

        gameManager.AddScore(scoreValue);
        Destroy(other.gameObject);
        Destroy(gameObject);
    }
}
