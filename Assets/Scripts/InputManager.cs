using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public playerMovement playMove;

    // Update is called once per frame
    void Update()
    {
        //player movement
        playMove.directionalInput();

        if (Input.GetKey(KeyCode.Space) && playMove.isGrounded)
        {
            playMove.jump();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            playMove.speed = 16;
            playMove.airSpeed = 12;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            playMove.speed = 12;
            playMove.airSpeed = 6;
        }









    }
}
