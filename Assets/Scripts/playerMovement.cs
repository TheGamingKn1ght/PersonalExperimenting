using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float jumpHeight = 3f;
    private float height;
    private Vector3 scale; 

    //Movement variables
    private float x, z;
    public float maxSpeed = 16; //Need to try and limit diagonal running
    public float speed = 12;
    public float airSpeed = 12;

    //GroundCheck Variables
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public bool isGrounded;

    //Gravity variables
    public float gravity = -9.81f;
    Vector3 velocity;

    private void Start()
    {
        height = controller.height;
        scale = this.transform.localScale;
    }
    // Update is called once per frame
    void Update()
    {
        //GroundCheck
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (isGrounded)
        {
            //Control player movement
            Vector3 move = (transform.right * x) + (transform.forward * z);
            controller.Move(move * speed * Time.deltaTime);
        }
        else
        {
            //Control player air movement
            Vector3 move = (transform.right * x) + (transform.forward * z);
            controller.Move(move * airSpeed * Time.deltaTime);
        }
        

        //Apply gravity to the player
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    public void directionalInput()
    {
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");
    }

    public void jump()
    {
        //Kinetic Energy + Potential Energy formulas together
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }

    public void StartCrouching()
    {
        //player height
        Vector3 tempScale = this.transform.localScale;
        tempScale.y *= 0.5f;
        this.transform.localScale = tempScale;
        controller.height *= 0.5f;

        //speed
        speed *= 0.25f;
        airSpeed *= 0.25f;
    }
    public void StopCrouching()
    {
        //player height
        this.transform.localScale = scale;
        controller.height = height;

        //speed
        speed *= 4f;
        airSpeed *= 4f;
    }
}
