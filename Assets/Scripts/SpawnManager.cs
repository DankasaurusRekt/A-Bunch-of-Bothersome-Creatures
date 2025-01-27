using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SpawnManager : MonoBehaviour
{
    //animal variables
    public GameObject[] catsPrefabs;

    //objective variables
    public GameObject objectivePrefab;
    public float objectiveCount;

    //animal spawn variables
    [SerializeField] private Vector3[] spawnPositions = new Vector3[]
    {
    new Vector3(-24, 0, -24),
    new Vector3(-20, 0, -24),
    new Vector3(-16, 0, -24),
    new Vector3(-12, 0, -24),
    new Vector3(-8, 0, -24),
    new Vector3(-4, 0, -24),
    new Vector3(0, 0, -24),
    new Vector3(4, 0, -24),
    new Vector3(8, 0, -24),
    new Vector3(12, 0, -24),
    new Vector3(16, 0, -24),
    new Vector3(20, 0, -24),
    new Vector3(24, 0, -24),
    };
    [SerializeField] private float spawnRate = 1f;
    [SerializeField] private float spawnRateIncrease;

    //objective spawn variables
    [SerializeField] private float spawnRangeX;
    [SerializeField] private float spawnRangeZ;

    //game management variables
    public GameManager gameManager; //variable is assigned in the editor

    // Start is called before the first frame update
    void Start()
    {
        objectiveCount = 0;
    }

    // Update is called once per frame
    void Update()
    {       
        CheckForObjectives();
    }

    public IEnumerator SpawnAnimals() //while the game is active, will spawn in animals at a certain rate
    {
        while (gameManager != null && gameManager.isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            Vector3 rotation = transform.eulerAngles; //defines a vector3 that will be used for rotation
            rotation.y = 0;
            Instantiate(catsPrefabs[RandomAnimalPrefab()], RandomAnimalSpawnPosition(), Quaternion.Euler(rotation));
        }         
    }

    public void CheckForObjectives() //checks to see if there are any objectives on the map, and if not, spawns a new one
    {
        if (gameManager != null && gameManager.isGameActive)
        {
            objectiveCount = GameObject.FindGameObjectsWithTag("Objective").Length; //finds all objects in the scene that have the tag type Enemy

            if (objectiveCount == 0)
            {
                SpawnObjective();
            }
        }
    }

    public void CalculateSpawnRate() //increases the spawn rate of the enemies by multiplying the current spawn rate by the spawnrateincrease, which is < 1
    {
        float calculatedSpawnRate;

        calculatedSpawnRate = spawnRate * spawnRateIncrease;

        spawnRate = calculatedSpawnRate;
    }

    void SpawnObjective() //spawns an objective randomly within the allocated area
    {
        Instantiate(objectivePrefab, RandomObjectiveSpawnPosition(), objectivePrefab.transform.rotation);
    } 

    int RandomAnimalPrefab() //helper method that returns a random animal assigned to the array
    {
        int animalIndex = Random.Range(0, catsPrefabs.Length);
        return animalIndex;
    }

    public Vector3 RandomAnimalSpawnPosition() //helper method that returns a random spawn position from the pre-defined array
    {
        int spawnIndex = Random.Range(0, spawnPositions.Length); // Generate a random index
        return spawnPositions[spawnIndex];                      // Return the Vector3 at the random index
    }

    private Vector3 RandomObjectiveSpawnPosition() //helper method that returns a vector3 coodrinate after randomly creating one
    {
        float spawnX = Random.Range(-spawnRangeX, spawnRangeX); //creates variables for X and Z positions to define spawns
        float spawnZ = Random.Range(-spawnRangeZ, spawnRangeZ);

        Vector3 spawnPosition = new Vector3(spawnX, 0, spawnZ);

        return spawnPosition;
    }
}
