using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine 
{
    public BaseState currentState;
    public StateMachine(BaseState initState)
    {
        this.currentState = initState;
        this.currentState.Enter();
    }
    public void Update()
    {
        this.currentState.Update();
    }
    public void ChangeState(BaseState newState)
    {
        this.currentState.Exit();
        this.currentState = newState;
        this.currentState.Enter();
    }
}
