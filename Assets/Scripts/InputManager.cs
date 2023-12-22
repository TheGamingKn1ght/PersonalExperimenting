using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public playerMovement playMove;

    // Update is called once per frame
    void Update()
    {
        playMove.directionalInput();

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            //movement
        }

        if (Input.GetKey(KeyCode.Space))
        {
            //Jump
        }

        








    }
}
