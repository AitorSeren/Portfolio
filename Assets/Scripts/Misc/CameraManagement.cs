using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraManagement : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField, Range(0,1)] float highValue = 0.0f;
    public float rotationSpeed = 1.0f;
    public float movementSpeed = 6.0f;
    public float minVerAngle = -45.0f;
    public float maxVerAngle = 45.0f;
    private float angleX = 0;
    private float angleY = 0;
    private float maxDistance = 5f;
    private Vector3 distance;

    //private float xVelocity, yVelocity, zVelocity, smoothTime;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        distance = transform.position - target.position;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.LookAt(target.position);
        //FocusOnTarget();
        ChangeCameraDistance();
        RotateAroundPlayer();
        //TrackTargetMovement();
    }

    void ChangeCameraDistance()
    {
        maxDistance = Mathf.Clamp(maxDistance + Input.GetAxis("Mouse ScrollWheel") * -1, 0, 10);
    }

    void RotateAroundPlayer()
    {
        /*
        transform.RotateAround(target.position, Vector3.up, Input.GetAxis("Mouse X") * Time.deltaTime * angleX);
        transform.RotateAround(target.position, Vector3.right, -1 * Input.GetAxis("Mouse Y") * Time.deltaTime * angleY);

        transform.rotation = new Quaternion(Mathf.Clamp(transform.rotation.x, -80f,80f), transform.rotation.y, transform.rotation.z, transform.rotation.w);
        */

        angleX += Input.GetAxis("Mouse Y");
        angleX = Mathf.Clamp(angleX, minVerAngle, maxVerAngle);
        angleY += Input.GetAxis("Mouse X");

        var targetRotation = Quaternion.Euler(angleX, angleY, 0);

        transform.position = target.position - targetRotation * Vector3.forward * maxDistance;
        transform.rotation = targetRotation;
    }
    /*
    void TrackTargetMovement()
    {
        distance = transform.position - target.position;
        distance.Normalize();
        if (Input.GetAxis("Vertical") != 0)
        {
            distance.y = Mathf.Clamp(distance.y + (1 * Time.deltaTime), 0, highValue);
        }
        distance = distance * maxDistance;
        float newPositionX = Mathf.SmoothDamp(transform.position.x, target.position.x + distance.x, ref xVelocity, smoothTime);
        float newPositionZ = Mathf.SmoothDamp(transform.position.z, target.position.z + distance.z, ref zVelocity, smoothTime);
        float newPositionY = Mathf.SmoothDamp(transform.position.y, target.position.y + distance.y, ref yVelocity, smoothTime);
        transform.position = new Vector3(newPositionX, newPositionY, newPositionZ);
    }*/
}
