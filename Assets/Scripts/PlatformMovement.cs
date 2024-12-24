using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using Utilities;

public class PlatformMovement : MonoBehaviour
{
    public int type = 0;
    public float movementSpeed, timeToStartMovement, distance;
    Timer timer;
    Vector3 startingPosition;

    // Start is called before the first frame update
    void Start()
    {
        timer = new CountdownTimer(distance/movementSpeed);
        startingPosition = transform.position;
        PlatformType();
    }

    // Update is called once per frame
    void Update()
    {
        timer.Tick(Time.deltaTime);
    }

    private void PlatformType()
    {
        switch(type)
        {
            case 1:

                break;

            case 2:
                break;

            case 3:
                break;

            default:
                StartCoroutine(PlatformScheduleDefault());
                break;
        }
    }

    IEnumerator PlatformScheduleDefault()
    {

        float changeDirection = 1;
        yield return new WaitForSeconds(timeToStartMovement);
        timer.Start();
        while (true)
        {

            if (!timer.isRunning)
            {
                changeDirection = -changeDirection;

                timer.Start();
            }

            PlatformMoves(Vector3.up, changeDirection * movementSpeed);
            yield return null;
        }
    }

    private void PlatformMoves(Vector3 direction, float speed)
    {
        transform.Translate(direction * speed * (0.5f + timer.Progress()) * Time.deltaTime, Space.World);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.transform.parent = transform;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.parent = null;
        }
    }
}
