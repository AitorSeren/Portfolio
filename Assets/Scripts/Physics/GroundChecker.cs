using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] float groundDistance = 0.08f;
    [SerializeField] LayerMask groundLayers;


    public bool isGrounded
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
        isGrounded = Physics.CheckSphere(transform.position, groundDistance, groundLayers);
    }
}