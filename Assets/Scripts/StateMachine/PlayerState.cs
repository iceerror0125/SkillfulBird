using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState : BaseState
{
    protected Player player;
    public PlayerState(Player player) { this.player = player; }
    public override abstract void Enter();
    public override abstract void Update();
    public override abstract void Exit();
}

