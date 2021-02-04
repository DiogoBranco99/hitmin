using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;
    private float speed = 8f;
    public float gravity = -9.81f;
    public float jumpHeight = 1.2f;

    public Transform groundCheck;
    public float groundDistance = 1.5f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isSlowed;
    void Update()
    {
        if (isSlowed)
        {
            speed = 4f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    public void restoreSpeed()
    {
        isSlowed = false;
        speed = 8f;
    }

    public void Slow()
    {
        isSlowed = true;
        // wait x seconds and invoke restore speed
        Invoke("restoreSpeed", 3);

    }
}