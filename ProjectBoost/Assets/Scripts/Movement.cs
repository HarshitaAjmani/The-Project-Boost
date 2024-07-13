using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rigidBody;
    AudioSource audioSource;

    [SerializeField] float mainThrust = 500f;
    [SerializeField] float rotationThrust = 100f;

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
            rigidBody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
            if(!audioSource.isPlaying)
            {
                audioSource.Play();
            }  
        }
        else
        {
            audioSource.Stop();
        }
    }

    //rotate either right or left. no both together
    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRoation(rotationThrust);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRoation(-rotationThrust);
        }
    }

    void ApplyRoation(float rotationThisFrame)
    {
        rigidBody.freezeRotation = true; //freezing roation so we can manually roatate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rigidBody.freezeRotation = false; //unfreezing roation so we can manually roatate
    }
}

