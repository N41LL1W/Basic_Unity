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

public class PlayerBehaviour : CharacterBase
{
    //Movimentation
    //Camera
    [SerializeField] Transform playerCamera = null;
    [SerializeField] float mouseSensitivity = 3.5f;

    //Movimentation
    [SerializeField] float walkSpeed = 6.0f;
    [SerializeField] float runSpeed = 12.0f;

    //Gravity
    [SerializeField] float gravity = -13.0f;

    //Smooth walk
    [SerializeField] [Range(0.0f, 0.5f)] float moveSmoothTime = 0.3f;
    [SerializeField] [Range(0.0f, 0.5f)] float mouseSmoothTime = 0.03f;

    //Jumping
    float jumpHeight = 3f;

    [SerializeField] bool lockCursor = true; //Lock the coursor in the middle of screen

    float cameraPitch = 0.0f;
    
    //Gravity
    float velocityY = 0.0f;

    CharacterController controller = null;

    Vector2 currentDir = Vector2.zero;
    Vector2 currentDirVelocity = Vector2.zero;

    Vector2 currentMouseDelta = Vector2.zero;
    Vector2 currentMouseDeltaVelocity = Vector2.zero;

    //Animations
    private Animator animator;
    public float vertical;
    public float horizontal;
    
    private TypeCharacter type;

    protected void Start()
    {
        base.Start();
        currentLevel = PlayerStatsController.GetCurrentLevel();
        // PlayerStatsController.SetTypeCharacter(TypeCharacter.Archer);
        type = PlayerStatsController.GetTypeCharacter();

        basicStats = PlayerStatsController.Instance.GetBasicStats(type);

        //Movimentation
        controller = GetComponent<CharacterController>();
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        
        this .animator = GetComponent<Animator>();
    }
    
    protected void Update()
    {
        base.Update();
        Animations();
        this.vertical = Input.GetAxis("Vertical");
        this.horizontal = Input.GetAxis("Horizontal");

        //Movimentation
        UpdateMouseLook();
        UpdateMovement();
    }

    void UpdateMouseLook() //MOUSE MOVIMENTATION
    {
        Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        //Smoothing
        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, mouseSmoothTime);

        cameraPitch -= currentMouseDelta.y * mouseSensitivity;
        cameraPitch = Mathf.Clamp(cameraPitch, -90.0f, 90.0f); //Limitating camera to 90'

        playerCamera.localEulerAngles = Vector3.right * cameraPitch;
        transform.Rotate(Vector3.up * currentMouseDelta.x * mouseSensitivity);
    }

    //Movimentation
    void UpdateMovement() //KEYBOARD MOVIMENTATION 
    {
        Vector2 targetDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        targetDir.Normalize(); //Normalize the vector

        //Making walk more smooth - SmoothDamp
        currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, moveSmoothTime);

        //Gravity
        if (controller.isGrounded)
            velocityY = 0.0f;
        velocityY += gravity * Time.deltaTime;

        if (Input.GetButtonUp("Jump") && controller.isGrounded) {

            velocityY = Mathf.Sqrt(jumpHeight * -2.0f * gravity);
            AnimationController.Instance.PlayAnimation(AnimationStates.JUMP); // Animação de Pulo
            
        };

        //Velocity formula
        if (Input.GetKey(KeyCode.LeftShift) == true || Input.GetKey(KeyCode.RightShift) == true)
        {
            Vector3 velocity = (transform.forward * currentDir.y + transform.right * currentDir.x) * runSpeed + Vector3.up * velocityY; 

            controller.Move(velocity * Time.deltaTime);
            AnimationController.Instance.PlayAnimation(AnimationStates.RUN); // Animação de Correr
        }
        else
        {
            Vector3 velocity = (transform.forward * currentDir.y + transform.right * currentDir.x) * walkSpeed + Vector3.up * velocityY; 

            controller.Move(velocity * Time.deltaTime);   
        }
    }
    
    // Animações de Andar e parado
    void Animations()
    {
        this.animator.SetFloat("Vertical", vertical);
        this.animator.SetFloat("Horizontal", horizontal);
        
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            AnimationController.Instance.PlayAnimation(AnimationStates.WALK);
        }
        else if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
        {
            AnimationController.Instance.PlayAnimation(AnimationStates.IDDLE);
        }
    }
}
