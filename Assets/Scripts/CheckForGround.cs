using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForGround : MonoBehaviour
{
    public PlayerController player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    //Collision stuff
    void OnCollisionEnter2D(Collision2D col)
    {
        //Checking for ground
        if (col.gameObject.tag == "Ground")
        {
            player.isGrounded = true;
        }
    }
}