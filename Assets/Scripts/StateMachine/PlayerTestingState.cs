using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTestingState : PlayerState
{
    private float moveSpeed = 5;
    public PlayerTestingState(Player player) : base(player)
    {
    }

    public override void Enter()
    {
        player.ChangeToNoGravity();
        player.ChangeToZeroVelocity();
    }

    public override void Exit()
    {
    }

    public override void Update()
    {
        if (Input.GetKey(KeyCode.DownArrow))
        {
            player.MoveYAxis(-moveSpeed);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            player.MoveYAxis(moveSpeed);
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            player.ChangeToZeroVelocity();
        } 
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            player.ChangeToZeroVelocity();
        }
    }
}
