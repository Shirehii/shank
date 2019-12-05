using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForGround : MonoBehaviour //This script is used in both of the character's legs
{
    public PlayerController player; //The PlayerController script

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>(); //Find the PlayerController script
    }

    void OnCollisionEnter2D(Collision2D col) //Look for collision
    {
        //Checking for ground
        if (col.gameObject.tag == "Ground")
        {
            player.isGrounded = true; //mark the player as 'grounded'
        }
    }
}