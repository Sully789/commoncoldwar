using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public GameObject explosion;

    public int health;
    public int scoreValue;
    public int damage;
    private GameManager gameManager;



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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Instantiate(explosion, transform.position, transform.rotation);
        gameManager.AddScore(scoreValue);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player Shot"))
        {
            TakeDamage(damage);
            Destroy(other.gameObject);
        }
    }

}
