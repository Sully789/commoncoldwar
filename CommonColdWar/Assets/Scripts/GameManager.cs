/*
 * Sean O'Sullivan, K00180620, Cross Platform Games Development, CA1
 * GameManager.cs handles the UI and Scene Management
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject[] hazards;        //GameObject array used to keep all the possible objects that can be spawned
    public GameObject boss;             //GameObject to hold the Boss, can be changed for each scene
    public Vector3 spawnValues;         //Vector3 to hold the range of where the objects can be spawned
    public Text storyText;              //UI that displays the various narrative strings 
    public Text scoreText;              //UI that displays the score
    public Text gameOverText;           //UI that displays the Game Over message
    public Text creditsText;            //UI that displays the Credits message
    public Button restartButton;        //UI that displays the Restart button
    public Button startButton;          //UI that displays the Start button
    public Button quitButton;           //UI that displays the Quit button
    public Image logoImage;             //UI that displays the Logo image
    public Image backgroundImage;       //UI that displays the Background image
    public Scene scene;                 //Scene object used for Scene Management

    public int hazardCount;             //int used in Coroutine for the amount of objects to be spawned stored
    public int scoreTarget;             //int used in Coroutine to determine when to spawn the Boss
    public int bossScoreTarget;         //int used to start the next Scene after the boss has been defeated
    public float spawnWait;             //float value used in Coroutine to start spawning objects
    public float startWait;             //float value used in Coroutine to start spawning objects
    public float waveWait;              //float value used in Coroutine to spawn next wave

    private int score;                  //int value that holds the score
    private bool gameOver;              //bool used to determine if game is over
    private bool restart;               //bool used to determine if game has been restarted
    private bool shieldPowerUpActive = false;      //bool used to determine if Shield Power Up is active
    private bool shotPowerUpActive = false;        //bool used to determine if Shot Power Up is active
    //Various text strings that are used throughout the game
    private string intro1StoryText = "Intruder Alert!\n Rhinovirus Detected!";
    private string mucusStoryText = "The first line of defense, the body produces Mucus to flush out the virus";
    private string intro2StoryText = "Mucus Cluster has been destroyed! Send in the Macrophages!";
    private string macrophageStoryText = "A white-blood cell know as a Macrophage is then sent to overwhelm the virus";
    private string intro3StoryText = "Macrophage Overwhelmed! Release Messenger Proteins to the Attack Cells!";
    private string bCellStoryText = "B Cells are tracker cells that seek out and find the virus";
    private string tCellStoryText = "T Cells are then sent to destroy the virus";
    private string memoryStoryText = "Once the Virus is defeated, a small number of B Cells and T Cells remain with a memory of the virus. If the virus were to return, the body can combat the intruder more effectively";
    private string endStoryText = "Remember to wash your hands regularly, avoid touching your nose or mouth and sneeze into a tissue or your elbow!";
    private string gameOverMessageText = "Game Over!";
    private string creditsMessageText = "Thank you for playing!";

    // Start is called before the first frame update
    void Start()
    {
        Opening();
        score = 0;
        UpdateScore();
        StartCoroutine(StoryText());
        StartCoroutine(SpawnWaves());
    }

    // Update is called once per frame
    void Update()
    {
        LoadNextLevel();
    }

    //Coroutine used to Spawn objects
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
            //if Game is over, turn of UI elements and stop spawning waves
            if (gameOver)
            {
                restartButton.gameObject.SetActive(true);
                quitButton.gameObject.SetActive(true);
                restart = true;
                break;
            }
            //if target score has been reached, spawn Boss and stop spawning waves
            if (score >= scoreTarget)
            {
                SpawnBoss();
                break;
            }

            yield return new WaitForSeconds(waveWait);
        }    
    }

    //Spawns the Boss in a random postion
    void SpawnBoss()
    {
        Vector3 spawnPosition = new Vector3(spawnValues.x, Random.Range(-spawnValues.y, spawnValues.y), spawnValues.z);
        Quaternion spawnRotation = Quaternion.identity;
        Instantiate(boss, spawnPosition, spawnRotation);
    }

    //Increments the score
    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    //Updates the Score UI
    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    //Sets Game Over and turns on UI
    public void GameOver()
    {
        gameOverText.text = gameOverMessageText;
        restartButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);
        gameOver = true;
    }

    //Restarts the current scene
    public void RestartGame()
    {
        SceneManager.LoadScene(scene.name);
    }

    //Turns off the opening UI elements
    public void Opening()
    {
        scene = SceneManager.GetActiveScene();
        gameOver = false;
        restart = false;
        scoreText.text = "";
        gameOverText.text = "";
        storyText.text = "";
        creditsText.text = "";
        restartButton.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);
    }

    //Turns off the Title Screen UI elements
    public void StartGame()
    {
        backgroundImage.gameObject.SetActive(false);
        logoImage.gameObject.SetActive(false);
        startButton.gameObject.SetActive(false);
    }

    //Brings Player back to Title Screen and turns on UI elements
    public void Quit()
    {
        backgroundImage.gameObject.SetActive(true);
        logoImage.gameObject.SetActive(true);
        gameOverText.text = "";
        restartButton.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);
        SceneManager.LoadScene(0);
    }

    //Turns on the Credits UI elements
    public void GameComplete()
    {
        //logoImage.gameObject.SetActive(true);
        backgroundImage.gameObject.SetActive(true);
        creditsText.text = creditsMessageText;
        storyText.text = endStoryText;
        quitButton.gameObject.SetActive(true);
    }

    //Checks if the target Score has been met and that the Boss has been defeated
    public void LoadNextLevel()
    {
        if (GameObject.FindWithTag("Boss") == null && score >= bossScoreTarget)
        {
            Debug.Log("Load Next Scene");
            if (scene.buildIndex <= 1) //If the buildindex is less then or equal 1 loads next scene
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

            }
            else if (scene.buildIndex == 2)//Else loads Game Complete method when build index is at 2
            {
                GameComplete();
            }
        }
    }

    //Coroutine used to display Narrative objects depending on build Index of Scene
    IEnumerator StoryText()
    {
        yield return new WaitForSeconds(waveWait);
        
        switch (scene.buildIndex)
        {
            case 0:
                storyText.text = intro1StoryText;
                yield return new WaitForSeconds(waveWait);
                storyText.text = mucusStoryText;
                break;

            case 1:
                storyText.text = intro2StoryText;
                yield return new WaitForSeconds(waveWait);
                storyText.text = macrophageStoryText;
                break;

            case 2:
                storyText.text = intro3StoryText;
                yield return new WaitForSeconds(waveWait);
                storyText.text = bCellStoryText;
                yield return new WaitForSeconds(waveWait);
                storyText.text = tCellStoryText;
                break;

            case 3:
                storyText.text = memoryStoryText;
                yield return new WaitForSeconds(waveWait);
                storyText.text = endStoryText;
                break;

            default:
                Debug.Log("Index not found");
                break;
        }
        yield return new WaitForSeconds(waveWait);
        storyText.text = "";
    }

    //Get method used to check if Shield Power Up is active
    public bool getShieldActive()
    {
        return shieldPowerUpActive;
    }

    //Get method used to check if Shot Power Up is active
    public bool getShotActive()
    {
        return shotPowerUpActive;
    }

    //Set method to set the Shield Power Up to active
    public void setShieldActive(bool isShieldPowerUpActive)
    {
        shieldPowerUpActive = isShieldPowerUpActive;
    }

    //Set method to set the Shot Power Up to active
    public void setShotActive(bool isShotPowerUpActive)
    {
        shotPowerUpActive = isShotPowerUpActive;
    }

}
