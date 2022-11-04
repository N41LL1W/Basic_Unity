using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeCharacter
{
    Warrior = 0,
    Wizard = 1,
    Archer = 2
}

//[RequireComponent(typeof(CharacterController))]
//[RequireComponent(typeof(Animator))]
public class PlayerBehaviour : CharacterBase
{
    private TypeCharacter type;
    
    // Animations
    private CharacterController characterController;
    private Animator animator;
    [HideInInspector]
    public bool canControl = true;
    
    // Movimentation
    public float vertical;
    public float horizontal;
    public float running;
    //public float speed = 3.0F;
    public float rotateSpeed = 3.0F;
    public CharacterController controller;
    public bool groundedPlayer;
    public Vector3 playerVelocity;
    public float jumpHeight = 1.0f;
    public float gravityValue = -9.81f;
    public float playerSpeed = 2.0f;
    
    protected void Start()
    {
        base.Start();
        currentLevel = PlayerStatsController.GetCurrentLevel();
        // PlayerStatsController.SetTypeCharacter(TypeCharacter.Archer);
        type = PlayerStatsController.GetTypeCharacter();

        basicStats = PlayerStatsController.Instance.GetBasicStats(type);

        controller = gameObject.AddComponent<CharacterController>();
        this.animator = GetComponent<Animator>();
    }
    
    void Update()
    {
        if (!this.canControl)
        {
            return;
        }

        this.vertical = Input.GetAxis("Vertical");
        this.horizontal = Input.GetAxis("Horizontal");
        this.Animations();
        this.Movimention();
        this.Run();
    }

    private void Animations()
    {
        this.animator.SetFloat("Vertical", vertical);
        this.animator.SetFloat("Horizontal", horizontal);
    }

    private void Movimention()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
        
        // Rotate around y - axis
        //transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0);
    }

    public void Run()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            this.animator.SetInteger("Shift", 1);
        }
        else
        {
            this.animator.SetInteger("Shift", 0);
        }
    }
}
