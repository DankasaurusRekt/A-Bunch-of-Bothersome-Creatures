using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //game management variables
    public GameManager gameManager; //variable is assigned in the editor

    //variables
    [SerializeField] private float speed;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveForward();
        CheckPosition();
    }

    public void CheckPosition() //checks if the cat positions are above the screen, and if so, deletes the object
    {
        if (transform.position.z > 25)
        {
            Destroy(gameObject);
        }
    }

    public void MoveForward() //moves the cats forward
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    public void OnCollisionEnter(Collision collision) // Check if the object colliding is the player, and if so, triggers the game over sequence
    {
        if (gameManager != null && collision.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            gameManager.GameOver(); //Triggers the game over sequence
        }
    }
}
