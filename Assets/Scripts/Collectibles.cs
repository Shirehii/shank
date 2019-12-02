using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectibles : MonoBehaviour
{
    //Floating variables
    private Vector3 startingPosition;
    private Vector3 targetPosition;
    private float distance = 0.4f;
    private float timeToReachTarget = 0.4f;
    private float t = 0;
    private int direction = 1; //1 is up, -1 is down

    private AudioSource source;
    public PlayerController player;

    void Start()
    {
        startingPosition = transform.position;
        targetPosition = startingPosition + Vector3.up * distance;
        
        source = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }


    void Update()
    {
        t += Time.deltaTime / timeToReachTarget;

        if (direction == 1)
        {
            transform.position = Vector3.Lerp(startingPosition, targetPosition, t);
            if (transform.position == targetPosition)
            {
                direction *= -1;
                t = 0;
            }
        }
        else if (direction == -1)
        {
            transform.position = Vector3.Lerp(targetPosition, startingPosition, t);
            if (transform.position == startingPosition)
            {
                direction *= -1;
                t = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            gameObject.SetActive(false);
        }
    }
}