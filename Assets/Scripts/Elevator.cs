using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    private bool goUp = false;
    private Vector3 startingPosition = new Vector3(-25, -32.5f, 0);
    private Vector3 targetPosition;
    private float distance = 52f;
    private float timeToReachTarget = 5f;
    private float t = 0;

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            goUp = true;
            t = 0;

            startingPosition = new Vector3(-25, -32.5f, 0);
            targetPosition = startingPosition + Vector3.up * distance;
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            t = 0;
            goUp = false;
        }
    }

    void Update()
    {
        if (goUp == true)
        {
            t += Time.deltaTime / timeToReachTarget;
            transform.position = Vector3.Lerp(startingPosition, targetPosition, t);
        }
        else if (goUp == false)
        {
            t += Time.deltaTime / timeToReachTarget;
            transform.position = Vector3.Lerp(transform.position, startingPosition, t);
        }
    }
}
