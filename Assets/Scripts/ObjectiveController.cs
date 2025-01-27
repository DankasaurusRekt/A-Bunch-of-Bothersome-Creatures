using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveController : MonoBehaviour
{
    //game management variables
    public GameManager gameManager; //variable is assigned in the editor
    public SpawnManager spawnManager;
    public AudioManager audioManager;

    //sfx variables
    
    

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        audioManager = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision) // Check if the object colliding is the player
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameManager.ScoreUp();
            spawnManager.CalculateSpawnRate();
            audioManager.PlayObjectiveCollectedSFX();
            Destroy(gameObject); // Destroys the object this script is attached to
        }
    }
}
