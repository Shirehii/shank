﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public PlayerController player; //The PlayerController script on the Player GameObject
    private Animator animator;

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        if (!player.paused && !player.won)
        {
            animator.speed = 1;
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
            animator.speed = 0;
        }
    }
}