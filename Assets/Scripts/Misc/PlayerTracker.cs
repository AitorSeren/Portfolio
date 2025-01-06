using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerTracker : MonoBehaviour
{
    [SerializeField, Range(0.00f,0.50f)] float smoothTime = 0.3f;                                                                         // If the smoothTime is higher, the tracker moves slower
    private float xVelocity = 0f;
    private float zVelocity = 0f;
    private float yVelocity = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            transform.position = other.transform.position;                                                             // If the tracker moves too far, it resets its position.
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            float newPositionX = Mathf.SmoothDamp(transform.position.x, other.transform.position.x, ref xVelocity, smoothTime); // When the tracker detects the player, it moves towards it using an SmoothDamp to get a smooth tracking
            float newPositionZ = Mathf.SmoothDamp(transform.position.z, other.transform.position.z, ref zVelocity, smoothTime);
            float newPositionY = Mathf.SmoothDamp(transform.position.y, other.transform.position.y, ref yVelocity, smoothTime);
            transform.position = new Vector3(newPositionX, newPositionY, newPositionZ);
        }
    }
}
