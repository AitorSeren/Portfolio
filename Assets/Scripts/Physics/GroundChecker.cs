using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour                      //  Checks if the GO is on the ground
{
    [SerializeField] float groundDistance = 0.08f;              //  Ground distance for the CheckSphere
    [SerializeField] LayerMask groundLayers;                    //  Layer that is considered ground


    public bool isGrounded                  //  Propierty that we can get publicly but it can only change privately.
    {
        get; private set;
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(transform.position, groundDistance, groundLayers);             // We check if there is any layer at the determined distance, true if there is.
    }
}