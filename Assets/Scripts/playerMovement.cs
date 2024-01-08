using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private CapsuleCollider playerCollider;
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

    //Colliding variables
    int maxBounces = 5;
    float skinWidth = 0.015f; //inside collider to start collision check
    private Bounds bounds;
    float maxSlopeAngle = 55;

    private void Start()
    {
        height = controller.height;
        scale = this.transform.localScale;
    }

    public void directionalInput()
    {
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");
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

        bounds = playerCollider.bounds;
        bounds.Expand(-2 * skinWidth);

        //Control player movement
        Vector3 move = (transform.right * x) + (transform.forward * z);
        Vector3 Collision = CollideAndSlide(move, transform.position, 0, move);
        controller.Move(move * speed * Time.deltaTime);

        /*
        if (isGrounded)
        {
            //Control player movement
            Vector3 move = (transform.right * x) + (transform.forward * z);
            controller.Move(move * speed * Time.deltaTime);
        }
        /*
        else
        {
            //Control player air movement
            Vector3 move = (transform.right * x) + (transform.forward * z);
            controller.Move(move * airSpeed * Time.deltaTime);
        }
        */

        //Apply gravity to the player
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private Vector3 CollideAndSlide(Vector3 vel, Vector3 startPos, int depth, Vector3 initVel) //depth is used to avoid a stack overflow and is compared to maxBounces
    {
        if(depth >= maxBounces)
        {
            return Vector3.zero;
        }

        float dist = vel.magnitude + skinWidth;

        RaycastHit hit;
        if(Physics.SphereCast(startPos, bounds.extents.x, vel.normalized, out hit, dist, groundMask))
        {
            Vector3 snapToSurface = vel.normalized * (hit.distance - skinWidth);
            Vector3 leftover = vel - snapToSurface;
            float angle = Vector3.Angle(Vector3.up, hit.normal);

            if(snapToSurface.magnitude <= skinWidth)
            {
                snapToSurface = Vector3.zero;
            }
            if (angle <= maxSlopeAngle)
            {
                leftover = ProjectAndScale(leftover, hit.normal);
            }
            //wall or steep slope
            else
            {
                //scale is the value that the velocity will be scaled accoridng to along the wall
                float scale = 1 - Vector3.Dot(new Vector3(hit.normal.x, 0, hit.normal.z).normalized, -new Vector3(initVel.x, 0, initVel.z).normalized);

                //to avoid jittering at the bottom of a slope
                //Treats it as a flat wall due to being grounded
                if (isGrounded)
                {
                    leftover = ProjectAndScale(new Vector3(leftover.x, 0, leftover.z), new Vector3(hit.normal.x, 0, hit.normal.z).normalized);
                    leftover *= scale;
                }
                else { leftover = ProjectAndScale(leftover, hit.normal) * scale; }
            }

            return snapToSurface + CollideAndSlide(leftover, Vector3.positiveInfinity + snapToSurface, depth + 1, initVel);
        }

        return vel;
    }
    
    private Vector3 ProjectAndScale(Vector3 vec, Vector3 normal)
    {
        //Project leftover Vector along the surface and scale it
        float mag = vec.magnitude;
        vec = Vector3.ProjectOnPlane(vec, normal).normalized;
        vec *= mag;
        return vec;
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
