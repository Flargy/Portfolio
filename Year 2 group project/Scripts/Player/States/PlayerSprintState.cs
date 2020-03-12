using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Main Author: Hjalmar Andersson

[CreateAssetMenu(menuName = "States/PlayerSprintState")]
public class PlayerSprintState : PlayerBaseState
{
    public override void Enter()
    {
        MovementTimer = 0.5f;
        acceleration = 20f;
        maxSpeed = 8f;
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
        if (Direction.magnitude == 0)
        {
            maxSpeed = 1.0f;
        }
        else
        {
            maxSpeed = 8.0f;
        }
        JumpInput();
        if (Jumped)
        {
            JumpTimer += Time.deltaTime;
            if(JumpTimer >= 0.5f)
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
            owner.TransitionTo<PlayerAirState>();
        else if (!Input.GetButton("Sprint") && Input.GetAxisRaw("Sprint") == 0)
        {
            owner.TransitionTo<PlayerGroundState>();
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
        if (VerticalDirection > MovementTimer)
        {
            VerticalDirection = MovementTimer;
        }
        if (HorizontalDirection > MovementTimer)
        {
            HorizontalDirection = MovementTimer;
        }
        Anim.SetFloat("Vertical", VerticalDirection * (Velocity.magnitude / topSpeed) );
        Anim.SetFloat("Horizontal", HorizontalDirection * (Velocity.magnitude / topSpeed));
    }


    public override void Exit()
    {
        maxSpeed = 3f;
    }
}
