using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerTracker : MonoBehaviour
{
    public float smoothTime = 0.3f;
    public float chameraHeight = 1.5f;
    private float xVelocity = 0;
    private float zVelocity = 0;
    private float yVelocity = 0;
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
            transform.Translate(other.transform.position - transform.position);
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            float newPositionX = Mathf.SmoothDamp(transform.position.x, other.transform.position.x, ref xVelocity, smoothTime);
            float newPositionZ = Mathf.SmoothDamp(transform.position.z, other.transform.position.z, ref zVelocity, smoothTime);
            float newPositionY = Mathf.SmoothDamp(transform.position.y, other.transform.position.y + chameraHeight, ref yVelocity, smoothTime);
            transform.position = new Vector3(newPositionX, newPositionY, newPositionZ);
        }
    }
}
