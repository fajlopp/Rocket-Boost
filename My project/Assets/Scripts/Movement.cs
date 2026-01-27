using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class Movement : MonoBehaviour
{
   [SerializeField] InputAction thrust;
   [SerializeField] float thrustStrength = 500f;
   [SerializeField] InputAction rotation;
   [SerializeField] float rotationStrength = 500f;
   [SerializeField] AudioClip mainEngine;
   
   AudioSource audioSource;
   Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
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
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngine);
            }
        }
        else
        {
            audioSource.Stop();
        }
    }

    void ProcessRotation()
    {
        float rotationInput = rotation.ReadValue<float>();

        if(rotationInput < 0)
        {
            ApplyRotation(rotationStrength);
        }
        else if(rotationInput > 0)
        {
            ApplyRotation(-rotationStrength);
        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.fixedDeltaTime);
        rb.freezeRotation = false;
    }
}
