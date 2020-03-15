using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DefaultPlayerController : BaseScript
{

    public Vector3 MinSpeed = new Vector3(0f, 0f, 2f);
    public float MaxSpeed = 15f;
    public float moveMultiplier = 10;
    public float JumpMultiplier = 50;

    public GameObject Player;

    private Rigidbody rigidbody;

    private InputMaster input;
    private EGameState previousState;
    private Vector2 movement;
    private const string horizontal = "Horizontal";
    private const string vertical = "Vertical";


    // Start is called before the first frame update
    void Awake()
    {
        this.input = new InputMaster();
        input.Player.Jump.performed += this.Jump;
        //input.Player.Movement.performed += ctx => this.Move(ctx.ReadValue<Vector2>());
        rigidbody = this.Player.GetComponent<Rigidbody>();
        this.gameManager.ContinueGame();
    }

    void Update()
    {
        if (this.rigidbody.position.y < -1)
        {
            this.gameManager.PauseGame();
        }
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        var forceToAdd = new Vector3();
        
        if (this.gameManager.IsRunning())
        {

            if (SystemInfo.supportsAccelerometer)
            {
                var input = Input.acceleration * this.moveMultiplier * Time.deltaTime;
            }
            
            var horizontalInput = input.x;
            var verticalInput = input.y;
            Debug.Log(input);

            forceToAdd += new Vector3(horizontalInput, 0f, verticalInput);


            if (this.rigidbody.transform.position.y > 3)
            {
                // increased gravity
                this.rigidbody.AddForce(0, -JumpMultiplier, 0, ForceMode.Impulse);
            }
        }
        else
        {
            this.rigidbody.velocity = new Vector3(0, 0, 0);
        }

        this.rigidbody.AddForce(forceToAdd);


        if (this.rigidbody.velocity.z > this.MaxSpeed)
        {
            this.rigidbody.velocity = new Vector3(this.rigidbody.velocity.x, this.rigidbody.velocity.y, this.MaxSpeed);
        }
    }

    private void Jump(InputAction.CallbackContext obj)
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

    private void OnEnable()
    {
        this.input.Enable();
        
    }

    private void OnDisable()
    {
        this.input.Disable();
    }
}
