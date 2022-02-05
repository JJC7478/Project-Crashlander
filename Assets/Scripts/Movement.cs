using System.Collections;
using System.Collections.Generic;
using UnityEngine; //<- namespace 

public class Movement : MonoBehaviour
//           ^name of class ^Movement is inheriting from MonoBehaviour, which is also a class with info from Unity


{
    // PARAMETERS - for tuning, typically set in the editor
    // CACHE - e.g. references for readability and speed
    // STATE - private instance (member) variables
    [SerializeField] float mainThrust = 100f; // can adjust the thrust power in Unity
    [SerializeField] float rotationThrust = 150f; // can adjust rotation speed in Unity
    [SerializeField] AudioClip mainThrusters;
    [SerializeField] ParticleSystem rocketThrustParticles;

    Rigidbody rb; 
    AudioSource audioSource; 


    void Start()
    {
      rb =  GetComponent<Rigidbody>(); // caching reference for Rigidbody
      audioSource = GetComponent<AudioSource>(); // caching reference for AudioSource
    }

    void Update()
    {

        ProcessThrust(); // method for rocket thrusting
        ProcessRotation(); // method for rocket rotation

    } 
    
void ProcessThrust() 
    {
       if (Input.GetKey(KeyCode.Space))
       // means if you are pressing/holding the spacebar...
       {
           rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
           // Adds force to rocket on the y-axis that is multiplied by mainThrust float variable and is frame rate independent
           if(!audioSource.isPlaying) // "!" means false; condition .isPlaying refers to if audio is already playing
           { 
            // ie, if audio is not already playing.....
            audioSource.PlayOneShot(mainThrusters);
            // play the audio when pressing/holding space
            if(!rocketThrustParticles.isEmitting)
            {
            rocketThrustParticles.Play();
            }
           }
           
           
       }
       else
       // if not pressing space...
       {
           audioSource.Stop();
           // stop the audio
           rocketThrustParticles.Stop();
       }

    }
    
    void ProcessRotation()
    {
       if (Input.GetKey(KeyCode.A))
       // when pressing A key...
        {
            ApplyRotation(rotationThrust);
            //rotate counter clockwise

        }
        else if (Input.GetKey(KeyCode.D))
        // when pressing D key...
       {
           ApplyRotation(-rotationThrust);
           // rotate clockwise
       }
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; //freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime); //rotates the rocket counterclockwise
        // rotationThisFrame is a float used for ApplyRotation to look for a float value to substitute for it in the transform.Rotate statement
        rb.freezeRotation = false; //unfreezing rotation so physics system can take over
    }
}   
