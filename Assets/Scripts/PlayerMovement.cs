using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed;
    Rigidbody rb;
    Vector3 direccionMovimiento;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        if(Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up * 10, ForceMode.Impulse);
        }
    }
    void FixedUpdate()
    {


        direccionMovimiento = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
        if (direccionMovimiento.magnitude != 0 )
        {
            transform.forward = new Vector3(Camera.main.transform.forward.x, transform.forward.y, Camera.main.transform.forward.z);
            rb.AddRelativeForce(direccionMovimiento * movementSpeed, ForceMode.Impulse);
        }
        else
        {
            //rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
    }
}
