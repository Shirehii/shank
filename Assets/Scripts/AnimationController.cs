using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public PlayerController player; //The PlayerController script
    private Animator animator; //The animator component

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>(); //Get the animator component
    }

    private void Update()
    {
        if (!player.paused && !player.won)
        {
            animator.speed = 1; //If the player hasn't won yet and hasn't paused the game, play animations
            if (player.dead)
            {
                animator.Play("Dead");
            }
            else if (!player.isGrounded)
            {
                animator.Play("Jumping");
            }
            else if (player.moving)
            {
                animator.Play("Moving");
            }
            else
            {
                animator.Play("Idle");
            }
        }
        else
        {
            animator.speed = 0; //If the player has won, or has paused the game, pause the animator
        }
    }
}