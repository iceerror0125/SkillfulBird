using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStateHandler : MonoBehaviour
{
    private Player player;
    private EPlayerState currentState = EPlayerState.Normal;
    #region State
    private StateMachine stateMachine;
    private PlayerNormalState normalState;
    private PlayerNoFallState noFallState;
    #endregion
    private Dictionary<EPlayerState, PlayerState> mappingState;

    private void OnEnable()
    {
        Observer.Instance.Subscribe(EventType.ChangePlayerState, ChangeStated);
        Observer.Instance.Subscribe(EventType.GetState, GetCurrentState);
    }
    private void OnDisable()
    {
        Observer.Instance.UnSubscribe(EventType.ChangePlayerState, ChangeStated);
        Observer.Instance.UnSubscribe(EventType.GetState, GetCurrentState);
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        player = PlayerManager.Instance.Player;

        InitState();

        MapState();
    }

    private void InitState()
    {
        normalState = new PlayerNormalState(player);
        noFallState = new PlayerNoFallState(player);
        stateMachine = new StateMachine(normalState);
    }

    private void MapState()
    {
        mappingState = new Dictionary<EPlayerState, PlayerState>() {
            {EPlayerState.Normal, normalState },
            {EPlayerState.Flash, noFallState }
        };
    }

    private void Update()
    {
        stateMachine.Update();
    }

    private void ChangeStated(Message msg)
    {
        if (msg.param is EPlayerState)
        {
            EPlayerState newState = (EPlayerState)msg.param;
            currentState = newState;

            if (mappingState.TryGetValue(newState, out var value))
            {
                stateMachine.ChangeState(value);
            }
        }
    }
    private void GetCurrentState(Message msg)
    {
        msg.returnValue = currentState;
    }
}
