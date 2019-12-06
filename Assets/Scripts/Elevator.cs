using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    private bool goUp = false; //If the elevator should start going up or not
    public Vector3 startingPosition; //Starting position - to be set in the inspector
    private Vector3 targetPosition; //Target position
    public float distance; //Distance between starting and target positions - to be set in the inspector
    private float timeToReachTarget = 5f; //Time to reach target position
    private float t = 0; //Time elapsed

    private void Start()
    {
        targetPosition = startingPosition + Vector3.up * distance; //Set target position
    }

    private void OnCollisionEnter2D(Collision2D col) //If the player stands on the elevator, go up
    {
        if (col.gameObject.tag == "Player")
        {
            goUp = true;
            t = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D col) //If the player leaves the elevator, stop going up
    {
        if (col.gameObject.tag == "Player")
        {
            goUp = false;
            t = 0;
        }
    }

    private void Update()
    {
        if (goUp == true) //Go up
        {
            t += Time.deltaTime / timeToReachTarget;
            transform.position = Vector3.Lerp(startingPosition, targetPosition, t);
        }
        else //Go down
        {
            t += Time.deltaTime / timeToReachTarget;
            transform.position = Vector3.Lerp(transform.position, startingPosition, t);
        }
    }
}