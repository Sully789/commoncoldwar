using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject[] hazards;
    public Vector3 spawnValues;
    public Text scoreText;
    public Text gameOverText;
    public GameObject restartButton;

    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    private int score;
    private bool gameOver;
    private bool restart;
    private bool shieldPowerUpActive;
    private bool shotPowerUpActive;

    void Start()
    {
        gameOver = false;
        restart = false;
        gameOverText.text = "";
        restartButton.SetActive(false);
        score = 0;
        UpdateScore();
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(spawnValues.x, Random.Range(-spawnValues.y, spawnValues.y), spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                restartButton.SetActive(true);
                restart = true;
                break;
            }
        }
        
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        gameOverText.text = "Game Over!";
        gameOver = true;
    }

    public void RestartGame()
    {
        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
    }

    public void ShieldActive(bool isShieldPowerUpActive)
    {
        shieldPowerUpActive = isShieldPowerUpActive;
    }

    public void ShotActive(bool isShotPowerUpActive)
    {
        shotPowerUpActive = isShotPowerUpActive;
    }
}
