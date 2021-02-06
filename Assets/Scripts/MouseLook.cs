using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity;
    public Transform playerBody;
    float xRotation = 0f;
    bool isPaused;

    // Start is called before the first frame update 
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isPaused = false;
    }

    // Update is called once per frame 
    void Update()
    {
        //playerBody = GameObject.FindWithTag("Player").transform;
        if (!isPaused) { 
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);

            //Rotate left and right;
            playerBody.Rotate(Vector3.up * mouseX);
        }
    }

    public void setPaused (bool condition)
    {
        isPaused = condition;
    }
    
}
