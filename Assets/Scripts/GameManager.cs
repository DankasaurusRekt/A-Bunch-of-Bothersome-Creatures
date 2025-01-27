using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//allows us to use Scene Management features
using TMPro; //allows us to use TMPro features
using UnityEngine.UI; //lets us interact with buttons

public class GameManager : MonoBehaviour
{
    //UI variables
    public TextMeshProUGUI scoreText; //creates a UI variable which we can use to display the score
    public TextMeshProUGUI gameOverText; //creates a UI variable which we can use to display game over
    public GameObject titleScreen;
    public Button restartButton; //creates a UI variable for the button
    public Button startButton;

    //scoring variables
    public int score;

    //game object variables
    private SpawnManager spawnManager;
    public AudioManager audioManager;
    public PlayerController playerController; //set in Editor

    //game management variables
    public bool isGameActive;


    // Start is called before the first frame update
    void Start()
    {
        SetGameInactive();
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>(); //finds the game object spawn manager
        audioManager = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ScoreUp() //adds 1 to the current score
    {
        score += 1;
        scoreText.text = "Score: " + score;
    }

    public void RestartGame()  //reloads the current scene
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GameOver() //triggers the game oversequence - turns on game over text.
    {
        SetGameInactive(); //sets the bool to false
        audioManager.PlayPlayerDeathSFX();
        gameOverText.gameObject.SetActive(true); //checks the active box on the gameobject to make it appear
        restartButton.gameObject.SetActive(true); //checks the active box on the gameobject to make it appear
    }

    public void StartGame() //triggers the game start sequence, turning off the title screen, turning on the score counter and spawning animals
    {
        SetGameActive();
        titleScreen.gameObject.SetActive(false);
        playerController.MakePlayerAppear();
        scoreText.text = "Score: " + score;
        scoreText.gameObject.SetActive(true);
        StartCoroutine(spawnManager.SpawnAnimals());
    }

    public void SetGameActive() //sets the game to active
    {
        isGameActive = true;
    }

    public void SetGameInactive() //sets the game to inactive
    {
        isGameActive = false;
    }

    public void OnButtonClick()
    {
        Debug.Log("Button clicked!");
    } //debug tool
}
