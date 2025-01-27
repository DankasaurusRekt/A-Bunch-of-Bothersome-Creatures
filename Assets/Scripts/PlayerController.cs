using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //move variables
    private float horizontalInput;
    private float verticalInput;
    [SerializeField] private float speed = 50.0f;

    //bound variables
    [SerializeField] private float zBound = 22.0f;
    [SerializeField] private float xBound = 39.0f;

    //game object variables
    public GameManager gameManager; //variable is assigned in the editor
    public AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        CheckOutOfBounds();
    }

    void PlayerMove() //tracks player movement if the game is active, and normalizes it
    {
        if (gameManager != null && gameManager.isGameActive)
        {
            horizontalInput = Input.GetAxis("Horizontal"); //Updates the horizontalInput variable to the be current value of left/right input (-1 to 1)
            verticalInput = Input.GetAxis("Vertical");

            var direction = (transform.forward * verticalInput) + (transform.right * horizontalInput); //creates a var that is a addition of both vectors * their input

            if (direction.magnitude > 1.0f) //normalises the var
            {
                direction.Normalize();
            }

            transform.Translate(direction * speed * Time.deltaTime);
        }
     }

    void CheckOutOfBounds() //checks if the player is in the bounds of the screen, and if not, shunts them back
    {
        if (transform.position.x < -xBound) 
        { 
            transform.position = new Vector3(-xBound, transform.position.y, transform.position.z);
        }
        if (transform.position.x > xBound)
        { 
            transform.position = new Vector3(xBound, transform.position.y, transform.position.z);
        }

        if (transform.position.z < -zBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -zBound);
        }
        if (transform.position.z > zBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zBound);
        }

    } 

    public void MakePlayerAppear() //makes the player visible
    {
        gameObject.SetActive(true);
    }

}
    

