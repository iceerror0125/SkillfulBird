using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNoFallState : PlayerState
{
    public PlayerNoFallState(Player player) : base(player)
    {
    }

    public override void Enter()
    {
        player.ChangeToNoGravity();
        player.TurnOffYVelocity();
    }

    public override void Exit()
    {
    }

    public override void Update()
    {
        player.ChangeToNoRotation();
    }
}
