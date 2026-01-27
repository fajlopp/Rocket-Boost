using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    
    [SerializeField] float loadDelay = 2f;
    [SerializeField] AudioClip crashObstacle;
    [SerializeField] AudioClip landOnPad;

    AudioSource audioSource;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void OnCollisionEnter(Collision other)
    {
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
        GetComponent<Movement>().enabled = false;
        audioSource.PlayOneShot(crashObstacle);
        Invoke("ReloadLevel", loadDelay);
    }

    void StartLoadSequence()
    {
        GetComponent<Movement>().enabled = false;
        audioSource.PlayOneShot(landOnPad);
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
