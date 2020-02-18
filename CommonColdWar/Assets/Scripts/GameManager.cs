using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject[] hazards;
    public GameObject boss;
    public Vector3 spawnValues;
    public Text storyText;
    public Text scoreText;
    public Text gameOverText;
    public GameObject restartButton;

    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public string introStoryText = "Intruder Alert! Rhinovirus Detected!";
    public string mucusStoryText = "The first line of defense, the body produces Mucus to flush out the virus";
    public string mucusBossStoryText = "Mucus Cluster has been destroyed! Send in the Macrophages!";
    public string macrophageStoryText = "A white-blood cell know as a Macrophage is then sent to overwhelm the virus";
    public string macrophageBossStoryText = "Macrophage Overwhelmed! Release Messenger Proteins to the Attack Cells!";
    public string bCellStoryText = "B Cells are tracker cells that seek out and find the virus";
    public string tCellStoryText = "T Cells are then send to destroy the virus";
    public string cellsBossStoryText = "Once the Virus is defeated, a small number of B Cells and T Cells remain with a memory of the virus. If the virus were to return, the body can combat the intruder more effectively";
    public string endStoryText = "Remember to wash your hands regulary, avoid touching your nose or mouth and sneeze into a tissue or your elbow!";


    private int score;
    private bool gameOver;
    private bool restart;
    private bool shieldPowerUpActive = false;
    private bool shotPowerUpActive = false;

    void Start()
    {
        gameOver = false;
        restart = false;
        gameOverText.text = "";
        storyText.text = "";
        restartButton.SetActive(false);
        score = 0;
        UpdateScore();
        StartCoroutine(SpawnWaves());
        StartCoroutine(StoryText());
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
            if(score > 100)
            {
                SpawnBoss();
                break;
            }
        }
        
    }

    IEnumerator StoryText()
    {
        yield return new WaitForSeconds(5);
        storyText.text = introStoryText;

    }

    void SpawnBoss()
    {
        Vector3 spawnPosition = new Vector3(spawnValues.x, Random.Range(-spawnValues.y, spawnValues.y), spawnValues.z);
        Quaternion spawnRotation = Quaternion.identity;
        Instantiate(boss, spawnPosition, spawnRotation);
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

    public bool getShieldActive()
    {
        //shieldPowerUpActive = isShieldPowerUpActive;
        return shieldPowerUpActive;
    }

    public bool getShotActive()
    {
       // shotPowerUpActive = isShotPowerUpActive;
        return shotPowerUpActive;
    }

    public void setShieldActive(bool isShieldPowerUpActive)
    {
        shieldPowerUpActive = isShieldPowerUpActive;
       // this.yourboolean = bool;
    }

    public void setShotActive(bool isShotPowerUpActive)
    {
        shotPowerUpActive = isShotPowerUpActive;
        //this.yourboolean = bool;
    }



}
