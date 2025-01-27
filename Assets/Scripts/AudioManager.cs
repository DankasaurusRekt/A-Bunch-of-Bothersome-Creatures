using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //game object variables
    public GameManager gameManager; //variable is assigned in the editor
    private AudioSource audioSource;

    //SFX clip variables
    public AudioClip objectiveCollectedSFX;
    public AudioClip playerDeathSFX;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayObjectiveCollectedSFX() //plays audio when an objective is collected
    {
        audioSource.PlayOneShot(objectiveCollectedSFX);
    }

    public void PlayPlayerDeathSFX() //plays audio when the player dies
    {
        audioSource.PlayOneShot(playerDeathSFX);
    }

}
