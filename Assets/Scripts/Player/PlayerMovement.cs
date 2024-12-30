using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed, jumpHeight, jumpDuration, launchPoint, gravityMultiplier, jumpForce, jumpCooldown;
    CountdownTimer jumpTimer, jumpCooldownTimer;
    Rigidbody rb;
    Vector3 direccionMovimiento;
    bool doubleJump;
    GroundChecker groundChecker;
    // Start is called before the first frame update
    void Awake()
    {
        jumpTimer = new CountdownTimer(jumpDuration);
        jumpCooldownTimer = new CountdownTimer(jumpCooldown);
        rb = GetComponent<Rigidbody>();
        doubleJump = false;
        groundChecker = GetComponent<GroundChecker>();
    }

    // Update is called once per frame
    private void Update()
    {
        HandleJump();
        HandleMovement();
        Ticker();
    }
    void FixedUpdate()
    {
        MoveCharacter();
    }

    void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && !jumpTimer.isRunning && !jumpCooldownTimer.isRunning && (groundChecker.isGrounded || doubleJump))
        {
            StartCoroutine(JumpAction());
            jumpTimer.Start();
        }
        else if (!Input.GetButton("Jump") && jumpTimer.isRunning)
        {
            jumpTimer.Stop();
        }
    }

    void Ticker()
    {
        jumpTimer.Tick(Time.deltaTime);
        jumpCooldownTimer.Tick(Time.deltaTime);
    }

    void HandleMovement()
    {
        direccionMovimiento = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
        if (direccionMovimiento.magnitude != 0)
        {
            transform.forward = new Vector3(Camera.main.transform.forward.x, transform.forward.y, Camera.main.transform.forward.z);
        }
    }

    void MoveCharacter()
    {

        rb.AddRelativeForce(direccionMovimiento * movementSpeed, ForceMode.VelocityChange);
    }

    IEnumerator JumpAction()
    {
        float jumpVelocity = 0.0f;
        while(!groundChecker.isGrounded || Input.GetButton("Jump"))
        {
            if (!jumpTimer.isRunning && groundChecker.isGrounded)
            {
                jumpVelocity = 0.0f;
                jumpTimer.Stop();
            }

            if (jumpTimer.isRunning)
            {
                if (jumpTimer.Progress() > launchPoint)
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
    }
}
