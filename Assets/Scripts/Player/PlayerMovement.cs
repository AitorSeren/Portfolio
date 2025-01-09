using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed, jumpHeight, jumpDuration, launchPoint, gravityMultiplier, jumpForce, jumpCooldown;                          //  These float modify the movement and jump values.
    private float animatorSpeed, velocity;                                                                                                  //  Float needed for animation purposes
    private float smoothTime = 0.2f;                                                                                                        
    CountdownTimer jumpTimer, jumpCooldownTimer;                                                                                            //  Timers used for the jump
    Rigidbody rb;                                                                                                                           
    Animator animator;
    Vector3 direccionMovimiento;                                                                                                            //  Vector3 that will register the player's input, the character will register it.
    bool doubleJump;                                                                                                                        //  boolean to register if double jump is available.
    GroundChecker groundChecker;                                                                                                            //  Checks if the character is touching the ground


    static readonly int Speed = Animator.StringToHash("Speed");                                                                             //  Some Static ints to communicate with the animator component
    static readonly int isJumping = Animator.StringToHash("isJumping");
    static readonly int isGrounded = Animator.StringToHash("isGrounded");
    static readonly int isDoubleJump = Animator.StringToHash("isDoubleJump");


    void Awake()
    {
        jumpTimer = new CountdownTimer(jumpDuration);
        jumpCooldownTimer = new CountdownTimer(jumpCooldown);
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        doubleJump = false;
        groundChecker = GetComponent<GroundChecker>();
    }


    private void Update()
    {
        UpdateAnimator();
        HandleJump();
        HandleMovement();
        Ticker();
    }
    void FixedUpdate()
    {
        MoveCharacter();
    }

    void HandleJump()                                                                                                                               //  Method that handles the input for jumps and its functions
    {
        if (Input.GetButtonDown("Jump") && !jumpTimer.isRunning && !jumpCooldownTimer.isRunning && (groundChecker.isGrounded || doubleJump))        //  We check the input and the conditions for the jump to be made
        {
            if(groundChecker.isGrounded)                                                                                                            //  If the jump was possible and the character was touching the ground
            {                                                                                                                                       //  the character can use the double jump.
                doubleJump = true;
                animator.SetBool(isDoubleJump, false);
            }
            else                                                                                                                                    //  If the character wasn't touching the ground, then the character 
            {                                                                                                                                       //  can't double jump after this jump.
                doubleJump = false;
                animator.SetBool(isDoubleJump, true);
            }
            animator.SetBool(isJumping, true);                                                                                                      
            StartCoroutine(JumpAction());                                                                                                           //  Coroutine for the jump
            jumpTimer.Start();                                                                                                                      //  We start the timer for the jump.
        }
        else if (!Input.GetButton("Jump") && jumpTimer.isRunning)                                                                                   //  If the Input is not pressed and the timer is running, the timer is stopped
        {
            jumpTimer.Stop();
        }
    }

    void Ticker()                                                                                                                                   //  Ticks all the timers (updated the time passed)
    {
        jumpTimer.Tick(Time.deltaTime);
        jumpCooldownTimer.Tick(Time.deltaTime);
    }

    void HandleMovement()                                                                                                                           //  Handles the inputs for the character movement
    {
        direccionMovimiento = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
        if (direccionMovimiento.magnitude != 0)                                                                                                     //  If the movement isn't 0, changes the forward of the character
        {
            direccionMovimiento = direccionMovimiento * movementSpeed;
            transform.forward = new Vector3(Camera.main.transform.forward.x, transform.forward.y, Camera.main.transform.forward.z);

            SmoothSpeed(direccionMovimiento.magnitude);                                                                                             //  We update the values for the animation with the magnitude
        }
        else
        {
            SmoothSpeed(0.0f);                                                                                                                      //  We update the values for the animation with a 0.0f to move down the animation
        }
        
    }

    void MoveCharacter()                                                                                                                            //  This moves the character towards the direction. If the input is not existing, there is no direction.
    {

        rb.AddRelativeForce(direccionMovimiento, ForceMode.VelocityChange);
    }


    IEnumerator JumpAction()                                                                                                                        //  Coroutine that makes the character jump
    {
        float jumpVelocity = 0.0f;  
        while(!groundChecker.isGrounded || Input.GetButton("Jump"))                                                                                 //  Works while the player is pressing the input or the character is not touching the ground
        {
            if (!jumpTimer.isRunning && groundChecker.isGrounded)                                                                                   //  Stops the timer if the ground is touched
            {
                jumpVelocity = 0.0f;
                jumpTimer.Stop();
            }

            if (jumpTimer.isRunning)
            {
                if (jumpTimer.Progress() > launchPoint)                                                                                             //  If the timer is counting and has passed the launch point, the jump goes down
                {
                    // This calculates the velocity required to reach the jump max height using the physic equation v = sqrt(2gh)
                    jumpVelocity = Mathf.Sqrt(2 * jumpHeight * Mathf.Abs(Physics.gravity.y));
                }
                else
                {
                    // Gradually apply less velocity as the jump progresses
                    jumpVelocity += (1 - jumpTimer.Progress()) * jumpForce * Time.fixedDeltaTime;
                }
            }
            else
            {

                // Gravity is now the only force applied
                if (rb.useGravity)
                {
                    jumpVelocity += Physics.gravity.y * gravityMultiplier * Time.fixedDeltaTime;
                }
                else
                {
                    jumpVelocity = 0.0f;
                }
            }

            // Apply velocity to rb
            rb.velocity = new Vector3(rb.velocity.x, jumpVelocity, rb.velocity.z);
            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }
        // When the character touches the ground, is no longer jumping.
        animator.SetBool(isJumping, false);
    }



    void SmoothSpeed(float value)                                                                                       //  Method that smooths the animations values
    {
        animatorSpeed = Mathf.SmoothDamp(animatorSpeed, value, ref velocity, smoothTime);
    }

    void UpdateAnimator()                                                                                               //  Method that updates all the animator processes or values
    {
        animator.SetFloat(Speed, animatorSpeed);
        animator.SetBool(isGrounded, groundChecker.isGrounded);
    }
}
