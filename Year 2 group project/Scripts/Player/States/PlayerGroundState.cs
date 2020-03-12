using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Main Author: Hjalmar Andersson

[CreateAssetMenu(menuName = "States/PlayerGroundState")]
public class PlayerGroundState : PlayerBaseState
{
    private bool increase;
    private bool jumped;
    public override void Enter()
    {
        jumped = false;
        if (MovementTimer <= 0.5f)
        {
            increase = true;
        }
        else if(MovementTimer > 1.0f)
        {
            increase = false;
        }
        MovementTimer = 0.0f;
        acceleration = 10f;
        maxSpeed = 3f;
        jumpForce = 5f;


        //Anim.SetBool("Jump", false);
        //Anim.SetBool("Falling", false);
    }

    public override void Update()
    {
        MovementTimer += Time.deltaTime / 2;
        MovementInput();
        AnimationHandeling();
        CameraDirectionChanges();
        ProjectToPlaneNormal();
        JumpInput();

        if (Direction.magnitude == 0)
        {
            maxSpeed = 1.0f;
        }
        else
        {
            maxSpeed = 3.0f;
        }

        if (Jumped)
        {
            JumpTimer += Time.deltaTime;
            if (JumpTimer >= 0.5f)
            {
                Jumped = false;
                JumpTimer = 0f;
            }
        }
        else
        {
            ControlDirection();
            GroundDistanceCheck();
        }
        
        ApplyGravity();
        Accelerate(Direction);
        DecelerateCheck();

        base.Update();

        if (Grounded() == false)
        {
            owner.TransitionTo<PlayerAirState>();
        }
        else if (Input.GetButton("Sprint") || Input.GetAxisRaw("Sprint") == 1)
        {
            owner.TransitionTo<PlayerSprintState>();
        }
    }


    private void ChangeGravityScale()
    {
        if (Grounded() == true)
        {
            gravity = 0;
        }
        else
        {
            gravity = 100f;
        }
    }

    /// <summary>
    /// Controlls the animation blending.
    /// </summary>
    private void AnimationHandeling()
    {
        if (increase == false && MovementTimer < 0.5f && VerticalDirection > 0)
        {
            VerticalDirection += 0.5f - MovementTimer;
        }
        if (increase == false && MovementTimer < 0.5f && HorizontalDirection > 0)
        {
            HorizontalDirection += 0.5f - MovementTimer;
        }

        Anim.SetFloat("Vertical", VerticalDirection * (Velocity.magnitude / (maxSpeed * 2)));
        Anim.SetFloat("Horizontal", HorizontalDirection * (Velocity.magnitude / (maxSpeed * 2)));
    }


    public override void Exit() { }
}
