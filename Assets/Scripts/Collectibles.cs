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

    //Points
    public int points = 0;

    //Healing & Damage
    public Lives lives;

    private AudioSource source;
    public PlayerController player;

    void Start()
    {
        startingPosition = transform.position;
        targetPosition = startingPosition + Vector3.up * distance;
        
        source = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
        lives = GameObject.FindGameObjectWithTag("Collectibles").GetComponent<Lives>();
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
            //Points
            if (gameObject.tag == "Point")
            {
                points += 1;
                Debug.Log(points);
                gameObject.SetActive(false);
            }

            //Adding HP
            if (gameObject.tag == "Heal")
            {
                gameObject.SetActive(false);
                lives.hearts += 1;
                source.PlayOneShot(player.heal);
            }

            //Death
            if (gameObject.tag == "Enemy" && lives.hearts > 0)
            {
                gameObject.SetActive(false);
                lives.hearts -= 1;
                source.PlayOneShot(player.damage);
            }
            else if (gameObject.tag == "Enemy" && lives.hearts == 0)
            {
                player.dead = true;
                gameObject.SetActive(false);
            }
        }
    }
}