using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public CharacterController controller;
    private float x, z;

    public float speed = 12;
   
    public float gravity = -9.81f;
    Vector3 velocity;
    
    // Update is called once per frame
    void Update()
    {
        Vector3 move = (transform.right * x) + (transform.forward * z);

        controller.Move(move * speed * Time.deltaTime);


        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    public void directionalInput()
    {
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");
    }

}
