using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public playerMovement playMove;

    // Update is called once per frame
    void Update()
    {
        //player movement updated every frame
        playMove.directionalInput();

        if (Input.GetKey(KeyCode.Space) && playMove.isGrounded)
        {
            playMove.jump();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.W))
        {
            playMove.speed = 16;
            playMove.airSpeed = 16;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            playMove.speed = 12;
            playMove.airSpeed = 12;
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            playMove.StartCrouching();
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            playMove.StopCrouching();
        }








    }
}
