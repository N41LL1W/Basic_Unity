using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeCharacter
{
    Warrior = 0,
    Wizard = 1,
    Archer = 2
}

public class PlayerBehaviour : CharacterBase
{
    private TypeCharacter type;
    private AnimationController animamationController;
    
    protected void Start()
    {
        base.Start();
        currentLevel = PlayerStatsController.GetCurrentLevel();
        // PlayerStatsController.SetTypeCharacter(TypeCharacter.Archer);
        type = PlayerStatsController.GetTypeCharacter();

        basicStats = PlayerStatsController.Instance.GetBasicStats(type);

        animamationController = GetComponent<AnimationController>();

    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            animamationController.PlayAnimation(AnimationStates.WALK);
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            animamationController.PlayAnimation(AnimationStates.RUN);
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            animamationController.PlayAnimation(AnimationStates.IDDLE);
        }
    }
}
