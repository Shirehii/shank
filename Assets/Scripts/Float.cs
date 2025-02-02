﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Float : MonoBehaviour
{
    private Vector3 startingPosition; //The object's starting position
    private Vector3 targetPosition; //The object's target position
    private float distance = 0.4f; //The distance between the starting and target positions
    private float timeToReachTarget = 0.4f; //The time to reach the target position
    private float t = 0; //The time that has passed
    private int direction = 1; //Direction that the object should go, 1 is up, -1 is down

    void Start()
    {
        startingPosition = transform.position; //Get starting position
        targetPosition = startingPosition + Vector3.up * distance; //Set target position
    }


    void Update()
    {
        t += Time.deltaTime / timeToReachTarget; //Time elapsed

        if (direction == 1) //Go up
        {
            transform.position = Vector3.Lerp(startingPosition, targetPosition, t);
            if (transform.position == targetPosition) //If the target position is reached, switch direction and reset time elapsed
            {
                direction *= -1;
                t = 0;
            }
        }
        else //Go down
        {
            transform.position = Vector3.Lerp(targetPosition, startingPosition, t);
            if (transform.position == startingPosition) //If the starting position is reached, switch direction and reset time elapsed
            {
                direction *= -1;
                t = 0;
            }
        }
    }
}