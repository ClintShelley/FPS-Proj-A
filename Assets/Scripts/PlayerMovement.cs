using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;

    //movement units
    public float speed = 7f;
    public float gravity = -29.43f;
    public float jumpHeight = 1f;
    public float sprintJumpHeight = 1.5f;
    public bool sprinting = false;

    Vector3 velocity;

    //touching floor check
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;

    //Foot step audio stuff
    float timeSinceStep = 0;
    AudioSource audioSource;
    Vector3 move;
    [SerializeField] AudioClip footstep1;
    [SerializeField] AudioClip footstep2;
    [SerializeField] AudioClip footstep3;
    [SerializeField] AudioClip footstep4;
    [SerializeField] AudioClip footstep5;
    [SerializeField] AudioClip footstep1S;
    [SerializeField] AudioClip footstep2S;
    [SerializeField] AudioClip footstep3S;
    [SerializeField] AudioClip footstep4S;
    [SerializeField] AudioClip footstep5S;
    [SerializeField] AudioClip Jumped;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //run function
        if (Input.GetKey(KeyCode.LeftShift) && isGrounded)
        {
            sprinting = true;
            speed = 10f;
        }
        else if (Input.GetButtonDown("Jump") && Input.GetKey(KeyCode.LeftShift))
        {
            sprinting = true;
            speed = 10f;
        }

        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            sprinting = false;
            speed = 7f;
        }
        else
        {
            sprinting = false;
            speed = 7f;
        }

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        //If touching ground allow jump
        if (Input.GetButtonDown("Jump") && Input.GetKey(KeyCode.LeftShift) && isGrounded)
        {
            audioSource.PlayOneShot(Jumped);
            velocity.y = Mathf.Sqrt(sprintJumpHeight * -2f * gravity);
        }
        else if (Input.GetButtonDown("Jump") && isGrounded)
        {
            audioSource.PlayOneShot(Jumped);
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        if (move.x != 0 && isGrounded)
        {
            processFootSteps();
        }
    }
    //generate a random audio step
    void processFootSteps()
    {
        timeSinceStep += Time.deltaTime;
        if (isGrounded == true && sprinting == false && timeSinceStep > 0.6)
        {
            int randomSound = Random.Range(1, 5);
            switch (randomSound)
            {
                case 1:
                    // print("playedsound1");
                    audioSource.PlayOneShot(footstep1);
                    break;
                case 2:
                    //print("playedsound2");
                    audioSource.PlayOneShot(footstep2);
                    break;
                case 3:
                    // print("playedsound3");
                    audioSource.PlayOneShot(footstep3);
                    break;
                case 4:
                    //  print("playedsound4");
                    audioSource.PlayOneShot(footstep4);
                    break;
                case 5:
                    //  print("playedsound5");
                    audioSource.PlayOneShot(footstep5);
                    break;
            }
            timeSinceStep = 0;
        }
        else if (isGrounded == true && sprinting == true && timeSinceStep > 0.3)
        {
            int randomSound = Random.Range(1, 5);
            switch (randomSound)
            {
                case 1:
                    // print("playedsound1");
                    audioSource.PlayOneShot(footstep1S);
                    break;
                case 2:
                    //print("playedsound2");
                    audioSource.PlayOneShot(footstep2S);
                    break;
                case 3:
                    // print("playedsound3");
                    audioSource.PlayOneShot(footstep3S);
                    break;
                case 4:
                    //  print("playedsound4");
                    audioSource.PlayOneShot(footstep4S);
                    break;
                case 5:
                    //  print("playedsound5");
                    audioSource.PlayOneShot(footstep5S);
                    break;
            }
            timeSinceStep = 0;
        }
    }
}
