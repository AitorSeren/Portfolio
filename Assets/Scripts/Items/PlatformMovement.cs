using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using Utilities;

public class PlatformMovement : MonoBehaviour
{
    public int type = 0;
    public float movementSpeed, timeToStartMovement, movementTime;
    Timer timer;
    Vector3 startingPosition;
    float changeDirection = 1;

    // Start is called before the first frame update
    void Start()
    {
        timer = new CountdownTimer(movementTime);
        timer.OnTimerStop = ChangeDirection;
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

        yield return new WaitForSeconds(timeToStartMovement);
        timer.Start();
        while (true)
        {      
            PlatformMoves(Vector3.up * changeDirection * movementSpeed);
            yield return null;
        }
    }

    void ChangeDirection()
    {
        changeDirection = -changeDirection;
        timer.Start();
    }

    private void PlatformMoves(Vector3 movement)
    {
        transform.Translate(movement * ((-1 * Mathf.Pow((timer.Progress() * 2) -1, 2f)) +1) * Time.deltaTime, Space.World);
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

