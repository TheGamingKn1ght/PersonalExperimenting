using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform playerHead;
    public Transform playerBody;
    
    private Transform playerCam;

    public float mouseSensitivity = 100f;
    private float xRotation = 0f;
    private float desiredx;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        this.transform.position = playerHead.transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        //update camera placement
        this.transform.position = playerHead.transform.position;

        //up and down
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        //side to side
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        //Equating playerCam to camera's transform
        playerCam = this.transform;
        Vector3 rotation = playerCam.transform.localRotation.eulerAngles;

        //xRotation is up and down camera movement, clamepd down to 90degrees
        //desiredx is for side to side movement
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        desiredx = rotation.y + mouseX;

        //Side to Side
        playerCam.localRotation = Quaternion.Euler(xRotation, desiredx, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
