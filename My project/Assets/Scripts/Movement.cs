using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class Movement : MonoBehaviour
{
   [SerializeField] InputAction thrust;
   [SerializeField] float thrustStrength = 500f;
   [SerializeField] InputAction rotation;
   

   Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void OnEnable()
    {
        thrust.Enable();
        rotation.Enable();
    }

    void ProcessThrust()
    {
        if (thrust.IsPressed())
        {
            rb.AddRelativeForce(Vector3.up * thrustStrength * Time.fixedDeltaTime);
        }
    }

    void ProcessRotation()
    {
        float rotationInput = rotation.ReadValue<float>();
        if(rotationInput < 0)
        {
            transform.Rotate(Vector3.forward);
        }
        else if(rotationInput > 0)
        {
            transform.Rotate(Vector3.back);
        }
    }


}
