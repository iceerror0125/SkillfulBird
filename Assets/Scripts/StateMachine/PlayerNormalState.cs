using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNormalState : PlayerState
{
    public PlayerNormalState(Player player) : base(player)
    {
    }

    public override void Enter()
    {
        player.ChangeToDefaultGravity();
    }

    public override void Exit()
    {
    }

    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            player.Jump();
        }

        if (player.rb.velocity.y < 0)
        {
            player.FallDown();
        }
        else
        {
            player.FallUp();
        }
    }
}
