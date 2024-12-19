using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraManagement : MonoBehaviour
{
    [SerializeField] Transform target;
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
    }

    // Update is called once per frame
    void Update()
    {
        FocusAtPlayer();
        //RotateAroundPlayer();
        //CalculateRotationAroundPlayer();
    }


    void FocusAtPlayer()
    {
        transform.LookAt(target.position);

        distance = transform.position - target.position;
        float distanceX = Mathf.Clamp(distance.x, -maxDistance, maxDistance);
        //float distanceY = Mathf.Clamp(distance.y, -maxDistance, maxDistance);
        float distanceZ = Mathf.Clamp(distance.z, -maxDistance, maxDistance);
        distance = new Vector3(distanceX, distance.y, distanceZ);
        transform.position = distance;
        if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
        {
            transform.RotateAround(target.position, Vector3.up, Input.GetAxis("Mouse X") * Time.deltaTime * angleX);
            transform.RotateAround(target.position, Vector3.right, Input.GetAxis("Mouse Y") * Time.deltaTime * angleY);
        }


        /*if ((protagonist.position - transform.position).magnitude > distanciaMax*3)
        {
            
            Vector3 direccion = new Vector3(protagonist.position.x - transform.position.x, 0, protagonist.position.z - transform.position.z);
            transform.position = direccion +transform.position;
        }*/
    }

    void RotateAroundPlayer()
    {
        float angleH = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        float angleV = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;
        transform.RotateAround(target.position, Vector3.up, angleH);
        transform.RotateAround(target.position, Vector3.right, angleV);
    }
    /*
    void CalculateRotationAroundPlayer()
    {
        transform.LookAt(target.position);

        distance = transform.position - target.position;
        float maxPointX,
              maxPointZ;

        float distanceX,
              distanceZ;

        if(Mathf.Abs(distance.z) >= maxDistance)
        {
            distanceZ = Mathf.Clamp(distance.z, -maxDistance, maxDistance);
            maxPointX = Mathf.Sqrt(Mathf.Pow(maxDistance, 2) + Mathf.Pow((distanceZ - target.position.z), 2)) + target.position.x;
            distanceX = Mathf.Clamp(distance.x, -maxPointX, maxPointX);
        }
        else if (Mathf.Abs(distance.x) >= maxDistance)
        {
            distanceX = Mathf.Clamp(distance.x, -maxDistance, maxDistance);
            maxPointZ = Mathf.Sqrt(Mathf.Pow(maxDistance, 2) + Mathf.Pow((distanceX - target.position.x), 2)) + target.position.z;
            distanceZ = Mathf.Clamp(distance.z, -maxPointZ, maxPointZ);
        }
        else
        {
            distanceX = Mathf.Clamp(distance.x, -maxDistance, maxDistance);
            distanceZ = Mathf.Clamp(distance.z, -maxDistance, maxDistance);
        }


        //float distanceY = Mathf.Clamp(distance.y, -maxDistance, maxDistance);
        distance = new Vector3(distanceX, 0, distanceZ);
        transform.position = distance;
    }*/
}
