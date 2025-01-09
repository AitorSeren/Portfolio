using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraManagement : MonoBehaviour
{
    [SerializeField] Transform target;                                  //      Camera's Target, it will be at the center of the screen.
    [SerializeField, Range(0, 10)] float maxDistance = 10;              //      Maximum distance for the camera.
    [SerializeField, Range(0, 10)] float minDistance = 0;               //      Minimum distance for the camera.
    [SerializeField] float cameraDisplacementY = 1.5f;                  //      Displacement for the camera on Height
    public bool invertAxisY = false;                                    //      Boolean that inverts the Y axis when set to true
    public bool invertAxisX = false;                                    //      Boolean that inverts the X axis when set to true
    private int invertedX = 1;
    private int invertedY = 1;

    public float minVerAngle = -45.0f;                                  //      Minimum Angle for rotation on X axis, making sure you cannot move the camera to undesired angles.
    public float maxVerAngle = 45.0f;                                   //      Maximum Angle for rotation on X axis, making sure you cannot move the camera to undesired angles.

    private float angleX = 0;                                           //      Float that stores the current rotation on X axis
    private float angleY = 0;                                           //      Float that stores the current rotation on Y axis
    private float distance = 5f;                                        //      Distance from the camera to the player.

    // Start is called before the first frame update
    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;                       //      We lock the cursor to make sure it doesn't go outside the screen.
        if(invertAxisX)                                             
        {
            invertedX = -1;                                             //      We invert the X axis, this int will change the final sign
        }

        if(invertAxisY)
        {
            invertedY = -1;                                             //      We invert the Y axis, this int will change the final sign
        }
    }

    // Update is called once per frame
    void Update()
    {
        ChangeCameraDistance();                                         //      Method that changes the distance of the camera to the target.
        RotateAroundPlayer();                                           //      Method that rotates the camera around the target and follows it.
    }

    void ChangeCameraDistance()
    {
        distance = Mathf.Clamp(distance + Input.GetAxis("Mouse ScrollWheel") * -1, minDistance, maxDistance);    // We change the distance using a Mathf.Clamp to make sure it doesn't go below to negative values or too far.
    }

    void RotateAroundPlayer()
    {
        angleX += Input.GetAxis("Mouse Y") * invertedX;
        angleX = Mathf.Clamp(angleX, minVerAngle, maxVerAngle);                     //      We change the angleX using the Input on Mouse Y with the angle limitations. We also invert the axis if necessary with "invertedX"
        angleY += Input.GetAxis("Mouse X") * invertedY;                             //      Same for angleY using the Input on Mouse X, but without limitations.

        Quaternion targetRotation = Quaternion.Euler(angleX, angleY, 0);       //      We create a rotation variable that saves the value of the new camera rotation.
        Vector3 targetDisplaced = new Vector3(target.position.x, target.position.y + cameraDisplacementY, target.position.z); // We apply the displacement desired for the camera
        transform.position =  targetDisplaced- targetRotation * Vector3.forward * distance; // The position of the camera is updated following the target at the established distance. We rotate a Vector3.forward to calculate the new position.
        transform.rotation = targetRotation;                                                // The rotation of the camera is updated.
    }
}
