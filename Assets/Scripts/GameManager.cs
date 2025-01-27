using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//allows us to use Scene Management features
using TMPro; //allows us to use TMPro features
using UnityEngine.UI; //lets us interact with buttons
using System.IO; //lets us use JSON and save data functionality

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
    public static GameManager instance; //the static keyword means that the value stored here (the Game Manager Objet) will be shared by all other instances of this class. Chances are I don't need to do this as I'm not managing data between scenes.

    //game management variables
    public bool isGameActive;

    // Start is called before the first frame update
    void Start()
    {
        SetGameInactive();
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>(); //finds the game object spawn manager
        audioManager = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
    }

    //Awake is called when the object is created
    private void Awake() 
    {
        //this is a singletone pattern that enables only 1 instance of the game manager to be kept throughout the game
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this; //stores the current instance of GameManger as the instance variable. I can get a link to this instance of the game object and I DON'T HAVE TO get a reference to it as I do above ^
        DontDestroyOnLoad(gameObject); //Makes this object not get destroyed when the scene loads
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

    [System.Serializable] //required to transform the following data to the JSON format
    class SaveData //a simple class that saves user data
    {
        public string name; //public variable used to store the player name
    }

    public void SaveName()
    {
        SaveData data = new SaveData(); //created a new instance of the SaveData class
        data.name = name; //fills the name class member with the name variable saved in Game Manager

        string json = JsonUtility.ToJson(data); //transforms this instance to JSON

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json); //writes the string to the data file
    }
}
