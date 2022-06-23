using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private float restartLevelDelay = 2f;
    [SerializeField] private AudioClip crashSFX;
    [SerializeField] private ParticleSystem crashParticle;

    [SerializeField] private float nextLevelDelay = 2f;
    [SerializeField] private AudioClip winSFX;
    [SerializeField] private ParticleSystem winParticle;

    private int currentSceneIndex;

    private Movement movement;
    private AudioSource audioSource;

    private bool isTransitioning = false;    

    private void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        movement = GetComponent<Movement>();
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        RespondToDebugKeys();
    }

    private void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
            NextLevel();                 
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isTransitioning)
            return;
        
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Friendly Object");
                break;
            case "Finish":
                Succes();                
                break;           
            default:               
                Crash();           
                break;
        }
    }
    private void Succes()
    {
        isTransitioning = true;

        audioSource.Stop();
        audioSource.PlayOneShot(winSFX);

        winParticle.Play();

        movement.enabled = false;
        Invoke("NextLevel", nextLevelDelay);
    }

    private void Crash()
    {
        isTransitioning = true;

        audioSource.Stop();
        audioSource.PlayOneShot(crashSFX);

        crashParticle.Play();             

        movement.enabled = false;
        Invoke("ResetLevel", restartLevelDelay);
    }
    
    private void NextLevel()
    {
        int nextLevel = currentSceneIndex + 1;
        if (nextLevel == SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(0);
        else
            SceneManager.LoadScene(nextLevel);
    }
    private void ResetLevel()
    {        
        SceneManager.LoadScene(currentSceneIndex);
    }
}
