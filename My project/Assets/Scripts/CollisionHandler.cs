using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    
    void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("You're on the friendly platform!");
                break;

            case "Finish":
                LoadNextLevel();
                break;

            default:
                ReloadLevel();
                break;
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

}
