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
   [SerializeField] ParticleSystem leftThruster;
   [SerializeField] ParticleSystem middleThruster;
   [SerializeField] ParticleSystem rightThruster;
   
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
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    private void StopThrusting()
    {
        audioSource.Stop();
        middleThruster.Stop();
    }

    private void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * thrustStrength * Time.fixedDeltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);

        }
        if (!middleThruster.isPlaying)
        {
            middleThruster.Play();
        }
    }

    void ProcessRotation()
    {
        float rotationInput = rotation.ReadValue<float>();

        if(rotationInput < 0)
        {
            RotateRight();

        }
        else if(rotationInput > 0)
        {
            RotateLeft();
        }
        else
        {
            StopRotating();
        }
    }

    private void StopRotating()
    {
        rightThruster.Stop();
        leftThruster.Stop();
    }

    private void RotateLeft()
    {
        ApplyRotation(-rotationStrength);
        if (!leftThruster.isPlaying)
        {
            rightThruster.Stop();
            leftThruster.Play();
        }
    }

    private void RotateRight()
    {
        ApplyRotation(rotationStrength);
        if (!rightThruster.isPlaying)
        {
            leftThruster.Stop();
            rightThruster.Play();
        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.fixedDeltaTime);
        rb.freezeRotation = false;
    }
}
