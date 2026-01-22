using UnityEngine;

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
                Debug.Log("Congrats, you've finished the stage!");
                break;

            case "Fuel":
                Debug.Log("Fuel picked up");
                break;
            default:
                Debug.Log("You crashed dummy");
                break;
        }

        
    }

}
