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
    public Animator animator;
    
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
                animator.SetBool("inJump", true);
            }
                break;
        }
    }

    void StopAnimations()
    {
        animator.SetBool("inIddle", false);
        animator.SetBool("inWalk", false);
        animator.SetBool("inRun", false);
        animator.SetBool("inJump", false);
    }
}
