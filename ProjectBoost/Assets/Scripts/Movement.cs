 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // PARAMETERS: for tuning, typically set in the editor
    // CACHE: e.g.references for readability or speed
    // STATE: private instance(member) variables

    [SerializeField] float mainThrust = 500f;
    [SerializeField] float rotationThrust = 100f;

    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem mainBooster;
    [SerializeField] ParticleSystem leftBooster;
    [SerializeField] ParticleSystem rightBooster;

    Rigidbody rigidBody;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    //rotate either right or left. no both together
    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            StopRoation();
        }
    }

    void StartThrusting()
    {
        rigidBody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        if (!mainBooster.isPlaying)
        {
            mainBooster.Play();
        }
    }
    void StopThrusting()
    {
        audioSource.Stop();
        mainBooster.Stop();
    }

    void RotateRight()
    {
        ApplyRoation(-rotationThrust);
        if (!leftBooster.isPlaying)
        {
            leftBooster.Play();
        }
    }

    void RotateLeft()
    {
        ApplyRoation(rotationThrust);
        if (!rightBooster.isPlaying)
        {
            rightBooster.Play();
        }
    }

    void StopRoation()
    {
        leftBooster.Stop();
        rightBooster.Stop();
    }

    void ApplyRoation(float rotationThisFrame)
    {
        rigidBody.freezeRotation = true; //freezing roation so we can manually roatate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rigidBody.freezeRotation = false; //unfreezing roation so we can manually roatate
    }
}

