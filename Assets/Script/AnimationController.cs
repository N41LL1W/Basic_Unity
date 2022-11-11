using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimationStates
{
    WALK,
    RUN,
    JUMP,
    IDDLE
}

public class AnimationController : MonoBehaviour
{
    public static AnimationController Instance;
    
    public Animator animator;

    void Start()
    {
        Instance = this;
    }

    public void PlayAnimation(AnimationStates stateAnimation)
    {
        switch (stateAnimation)
        {
            case AnimationStates.IDDLE:
            {
                StopAnimations();
                animator.SetBool("inIddle", true);
            }
                break;
            case AnimationStates.WALK:
            {
                StopAnimations();
                animator.SetBool("inWalk", true);
            }
                break;
            case AnimationStates.RUN:
            {
                StopAnimations();
                animator.SetBool("inRun", true);
            }
                break;
            case AnimationStates.JUMP:
            {
                StopAnimations();
                animator.SetTrigger("inJump");
            }
                break;
        }
    }

    public void StopAnimations()
    {
        animator.SetBool("inIddle", false);
        animator.SetBool("inWalk", false);
        animator.SetBool("inRun", false);
    }
}
