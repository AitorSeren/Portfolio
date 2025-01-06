using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using Utilities;

public class PlatformMovement : MonoBehaviour
{
    public int type = 0;                                                                // Platform type that defines the movement pattern
    public float movementSpeed, timeToStartMovement, movementTime;                      // Platform movement parameters.
    Timer timer;                                                                        // Timer for the platform.
    Vector3 startingPosition;                                                           // Vector3 that will set the starting position
    int changeDirection = 1;                                                            // int that will change directions if necessary.

    // Start is called before the first frame update
    void Start()
    {
        timer = new CountdownTimer(movementTime);                                       //  CountdownTimer that will change direction when finishes
        timer.OnTimerStop = ChangeDirection;                                            //  Function called when the timer finishes
        startingPosition = transform.position;                                          //  We store the starting position
        PlatformType();                                                                 //  We call the function that will start the coroutine movement
    }

    // Update is called once per frame
    void Update()
    {
        timer.Tick(Time.deltaTime);                                                     //  Tick from the timer
    }

    private void PlatformType()
    {
        switch(type)                                                                    //  The type decides the movement pattern
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

    IEnumerator PlatformScheduleDefault()                                               // The default movement
    {

        yield return new WaitForSeconds(timeToStartMovement);                           //  We wait for a specific time
        timer.Start();                                                                  //  The timer starts
        while (true)
        {      
            PlatformMoves(Vector3.up * changeDirection * movementSpeed);                //  The platform moves and updates frame.
            yield return null;
        }
    }

    void ChangeDirection()
    {
        changeDirection = -changeDirection;                                             //  We change the direction and restart the timer
        timer.Start();
    }

    private void PlatformMoves(Vector3 movement)
    {
        transform.Translate(movement * ((-1 * Mathf.Pow((timer.Progress() * 2) -1, 2f)) +1) * Time.deltaTime, Space.World);     // This method moves th platform in a smooth way using the function f(y) = -(2x-1)^2 + 1
                                                                                                                                // This function is equal to 0 at the start, 0.5f at half and 1 at the end, but has a curve draw on it
                                                                                                                                // This will make the platform movement more natural and smooth
    }

}

