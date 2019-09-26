/*
 * Player movement, walking, jumping, and animation triggers
 * Author: lejara 
 * 
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{


    public bool isGrounded;
    //[HideInInspector]
    public bool isJumping = false;
    public bool stopMoving = false;
    public float movementSpeed = 10f;
    //[HideInInspector]     
    public float groundDetectionRadius;
    public float jumpInitalVelocity = 4f;
    public float jumpAddVelocity = 4f;
    public float jumpAmmount = 200f;
    public float decreaseAmmountBy = 2f;
    public Transform groundCheck;
    public LayerMask whatsGround;

    private bool jump_Input = false;
    private bool secondJump_Input = false;
    private bool canSecondJump = false;
    private float hor_Input = 0;
    private float currentJumpAmmount = 0;
    private Rigidbody2D rig;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckMoveInput();               
    }

    private void FixedUpdate()
    {
        //Check if player feet is touching the ground
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundDetectionRadius, whatsGround);
        
        JumpCheck();

        MoveSide();

        MovementAnimationTriggersSet();
    }

    //Check User Input for movement
    private void CheckMoveInput()
    {
        if (!stopMoving)
        {
            hor_Input = Input.GetAxis("Horizontal");

            jump_Input = Input.GetButton("Jump");

            secondJump_Input = Input.GetButtonDown("Jump");
        }
        else
        {
            hor_Input = 0;
            jump_Input = false;
        }
        

    }

    //Horizontal Player Movement
    public void MoveSide()
    {
        if (isGrounded)
        {
            rig.velocity = new Vector2(hor_Input * movementSpeed, rig.velocity.y);

        }
        else
        {
            rig.velocity = new Vector2((hor_Input * movementSpeed ) , rig.velocity.y);
        }

        if (hor_Input != 0)
        {
            spriteRenderer.flipX = hor_Input > 0 ? false : true;
        }
    }

    //Check if Player is ready to jump
    private void JumpCheck()
    {        //TODO: second jump needs fixing, it is used sometimes while in the normal jump
        if (isGrounded)
        {
            canSecondJump = true;
        }

        //Check if the character is not jumping already and is touching ground
        // If its ready for a jump, add a ammount for the jump
        if (jump_Input && !isJumping && isGrounded)
        {
            rig.velocity = new Vector2(rig.velocity.x, jumpInitalVelocity);
            currentJumpAmmount = jumpAmmount;
            AddJumpForce();

        }
        //Secound Jump OOF
        else if (!isGrounded && secondJump_Input && canSecondJump)
        {
            print("add secound jumpo");
            rig.velocity = new Vector2(rig.velocity.x, jumpInitalVelocity);
            canSecondJump = false;
        }
        if (isJumping) // keep calling jump until the jump has ended
        {
            AddJumpForce();

        }

    }
    private void AddJumpForce()
    {
        //Response for jumping. If the user is pressing the jump buttion and there is an ammount for a jump, velocity will be added to the y-axis.
        if (jump_Input && currentJumpAmmount > 0)
        {
            rig.velocity = new Vector2(rig.velocity.x, rig.velocity.y+jumpAddVelocity); // Add a bit of y velocity 
            isJumping = true;
            currentJumpAmmount -= decreaseAmmountBy;
        }
        else
        {
            //Stop adding force
            isJumping = false;
            //canSecondJump = true;
            currentJumpAmmount = 0;
 
        }
        //This if will cancel the jump if the character has hit his head on a object.
        if (isJumping && rig.velocity.y == 0)
        {
            isJumping = false;
            currentJumpAmmount = 0;
        }
    }

    private void MovementAnimationTriggersSet()
    {
        animator.SetBool("isGrounded", isGrounded);
        animator.SetBool("isMoving", Mathf.Abs(rig.velocity.x) > 0 );
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundDetectionRadius);
    }

}
