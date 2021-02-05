using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;
    private float speed;
    public float normalSpeed;
    public float slowedSpeed;
    public float gravity = -9.81f;
    public float jumpHeight = 1.2f;

    public Transform groundCheck;
    public float groundDistance = 1.5f;
    public LayerMask groundMask;
    AudioSource stepsSound;
    private float lastPosition;

    Vector3 velocity;
    bool isSlowed;

    void Start ()
    {
        stepsSound = GetComponent<AudioSource>();
        //lastPosition = Vector3.Distance(transform.position, transform.position);
    }

    void Update()
    {
        if (isSlowed)
        {
            speed = slowedSpeed;
        }
        else
        {
            speed = normalSpeed;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        //Invoke("FootstepSounds", 1.5f);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

    }

    public void restoreSpeed()
    {
        isSlowed = false;
    }

    public void Slow()
    {
        isSlowed = true;
        Invoke("restoreSpeed", 3);

    }

    /*
    void FootstepSounds()
    {

        if (transform.position + 0.5 > lastPosition)
        {
            stepsSound.Play();
        }

        lastPosition = currentPosition;
    }
    */
}