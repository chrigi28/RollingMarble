﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultPlayerController : BaseScript
{

    public Vector3 MinSpeed = new Vector3(0f, 0f, 2f);
    public float MaxSpeed = 15f;
    public float moveMultiplier = 10;
    public float JumpMultiplier = 50;

    public GameObject Player;

    private Rigidbody rigidbody;

    private EGameState previousState;
    private Vector2 movement;
    private const string horizontal = "Horizontal";
    private const string vertical = "Vertical";


    // Start is called before the first frame update
    void Awake()
    {
        //input.Player.Movement.performed += ctx => this.Move(ctx.ReadValue<Vector2>());
        rigidbody = this.Player.GetComponent<Rigidbody>();
        this.GameManager.ContinueGame();

        if (SystemInfo.supportsGyroscope)
        {
            Input.gyro.enabled = true;
        }
    }

    void Update()
    {
        if (this.rigidbody.position.y < -1)
        {
            this.GameManager.PauseGame();
        }
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        var forceToAdd = Vector3.zero;

        if (this.GameManager.IsRunning())
        {

            if (SystemInfo.supportsAccelerometer)
            {
                forceToAdd.x = Input.acceleration.x;
                forceToAdd.y = 0;
                forceToAdd.z = Input.acceleration.y;
            }
            else
            {
                forceToAdd.x = Input.GetAxis(horizontal);
                forceToAdd.z = Input.GetAxis(vertical);
            }

            if (this.rigidbody.transform.position.y > 3)
            {
                // increased gravity
                this.rigidbody.AddForce(0, -JumpMultiplier / 4, 0, ForceMode.Impulse);
            }

            if (Input.touchSupported)
            {
                
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    this.Jump();
                }
            }

        }
        else
        {
            this.rigidbody.velocity = new Vector3(0, 0, 0);
        }

        this.rigidbody.AddForce(forceToAdd * this.moveMultiplier * Time.deltaTime);


        if (this.rigidbody.velocity.z > this.MaxSpeed)
        {
            this.rigidbody.velocity = new Vector3(this.rigidbody.velocity.x, this.rigidbody.velocity.y, this.MaxSpeed);
        }
    }

    private void Jump()
    {

        if (this.rigidbody.transform.position.y < 1.02)
        {
            this.rigidbody.AddForce(0, JumpMultiplier, 0, ForceMode.Impulse);
            Debug.Log("JUMP");
        }
    }

    private void Move(Vector2 direction)
    {
        Debug.Log("Move" + direction);
        this.movement = direction * this.moveMultiplier * Time.deltaTime;
    }
}
