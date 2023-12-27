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

        








    }
}
