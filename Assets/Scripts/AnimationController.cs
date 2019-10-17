using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public PlayerController player;
    private Animator animator;

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        if (player.jumping)
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
}