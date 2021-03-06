﻿using UnityEngine;

namespace Assets.Scripts
{
    public class DefaultPlayerController : MonoBehaviour
    {

        public Vector3 MinSpeed = new Vector3(0f, 0f, 2f);
        public float MaxSpeed = 15f;
        public float moveMultiplier = 10;
        public float JumpMultiplier = 2;

        public GameObject Player;

        private Rigidbody rigidbody;

        private EGameState previousState;
        private Vector2 movement;
        private bool isGrounded;
        private Vector3 jumpForce = Vector3.zero;
        private bool jumping;
        private const string horizontal = "Horizontal";
        private const string vertical = "Vertical";


        // Start is called before the first frame update
        void Awake()
        {
            //input.Player.Movement.performed += ctx => this.Move(ctx.ReadValue<Vector2>());
            rigidbody = this.Player.GetComponent<Rigidbody>();
            if (SystemInfo.supportsGyroscope)
            {
                Input.gyro.enabled = true;
            }
        }

        void Update()
        {
            if (GameManager.Instance.IsRunning() && this.rigidbody.position.y < -1)
            {
                GameManager.Instance.GameOver();
            }
        }


        // Update is called once per frame
        void FixedUpdate()
        {
            var forceToAdd = Vector3.zero;

            if (GameManager.Instance.IsRunning())
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

                ////if (Input.touchSupported)
                ////{

                ////}
                ////else
                ////{
                ////    if (Input.GetKeyDown(KeyCode.Space))
                ////    {
                ////        this.Jump();
                ////    }
                ////}

            }
            else
            {
                this.rigidbody.velocity = new Vector3(0, 0, 0);
            }

            if (this.rigidbody.transform.position.y > 3)
            {
                // increased gravity
                forceToAdd.y = -1;
            }

            this.rigidbody.AddForce(forceToAdd * this.moveMultiplier * Time.deltaTime);

            if (this.jumping)
            {
                this.jumping = false;
                this.rigidbody.AddForce(Vector3.up * Mathf.Sqrt(JumpMultiplier * -2f * Physics.gravity.y), ForceMode.VelocityChange);
            }

            if (this.rigidbody.velocity.z > this.MaxSpeed)
            {
                this.rigidbody.velocity =
                    new Vector3(this.rigidbody.velocity.x, this.rigidbody.velocity.y, this.MaxSpeed);
            }
        }

        public void Jump()
        {
            if (this.isGrounded)
            {
                this.jumping = true;
                this.isGrounded = false;
                ////this.rigidbody.AddForce(0, JumpMultiplier, 0, ForceMode.Impulse);
                ////var tempval = this.rigidbody.velocity;
                ////this.rigidbody.velocity = new Vector3(tempval.x, this.JumpMultiplier, tempval.z);
                Debug.Log("JUMP");
            }
        }

        private void Move(Vector2 direction)
        {
            Debug.Log("Move" + direction);
            this.movement = direction * this.moveMultiplier * Time.deltaTime;
        }

        void OnCollisionEnter(Collision collision)
        {
            this.isGrounded = true;
         }
    }
}
