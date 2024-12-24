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
    public float maxDistance = 5f;
    public float angleX = 180;
    public float angleY = 180;
    public Vector3 distance;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        distance = transform.position - target.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target.position);
        maxDistance = Mathf.Clamp(maxDistance + Input.GetAxis("Mouse ScrollWheel") * -1, 0, 10);
        RotateAroundPlayer();
    }

    void RotateAroundPlayer()
    {
        transform.RotateAround(target.position, Vector3.up, Input.GetAxis("Mouse X") * Time.deltaTime * angleX);
        transform.RotateAround(target.position, Vector3.right, -1 * Input.GetAxis("Mouse Y") * Time.deltaTime * angleY);

        distance = transform.position - target.position;
        distance.Normalize();
        if(Input.GetAxis("Vertical") != 0)
        {
            distance.y = Mathf.Clamp(distance.y + (1 * Time.deltaTime), 0, highValue);
        }
        transform.position = target.position + distance * maxDistance;
    }
}
