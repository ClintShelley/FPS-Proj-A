using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;

    //movement units
    public float speed = 7f;
    public float gravity = -29.43f;
    public float jumpHeight = 1f;
    public float sprintJumpHeight = 1.5f;

    Vector3 velocity;

    //touching floor check
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //run function
        if(Input.GetKey(KeyCode.LeftShift) && isGrounded)
        {
            speed = 10f;
        }
        else if (Input.GetButtonDown("Jump") && Input.GetKey(KeyCode.LeftShift))
        {
            speed = 10f;
        }

        else if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = 7f;
        }
        else
        {
            speed = 7f;
        }

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        //If touching ground allow jump
        if(Input.GetButtonDown("Jump") && Input.GetKey(KeyCode.LeftShift) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(sprintJumpHeight * -2f * gravity);
        }
        else if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
