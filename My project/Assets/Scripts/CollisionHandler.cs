using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    
    [SerializeField] float loadDelay = 2f;
    [SerializeField] AudioClip crashObstacle;
    [SerializeField] AudioClip landOnPad;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;

    AudioSource audioSource;

    bool isControllable = true;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void OnCollisionEnter(Collision other)
    {

        if (!isControllable) { return; }

        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("You're on the friendly platform!");
                break;

            case "Finish":
                StartLoadSequence();
                break;

            default:
                StartCrashSequence();
                break;
        }
    }

    void StartCrashSequence()
    {
        isControllable = false;
        audioSource.Stop();
        GetComponent<Movement>().enabled = false;
        crashParticles.Play();
        audioSource.PlayOneShot(crashObstacle);
        Invoke("ReloadLevel", loadDelay);
    }

    void StartLoadSequence()
    {
        isControllable = false;
        audioSource.Stop();
        GetComponent<Movement>().enabled = false;
        audioSource.PlayOneShot(landOnPad);
        successParticles.Play();
        Invoke("LoadNextLevel", loadDelay);
    }

    void LoadNextLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextScene = currentScene + 1;

        if (nextScene == SceneManager.sceneCountInBuildSettings)
        {
            nextScene = 0;
        }

        SceneManager.LoadScene(nextScene);
    }
    


    void ReloadLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
        }

}
