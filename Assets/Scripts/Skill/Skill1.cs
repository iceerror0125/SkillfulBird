using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill1 : Skill
{
    private float speedUp = -20;
    private float originSpeed;
    private float maxCounter = 3;
    private float counter = 0;
    public Skill1(int charmId) : base(charmId)
    {
    }

    public override void ActivateSkill()
    {
        PipePoolManager.Instance.ChangeSpeedGroup(Mathf.Abs(speedUp) * -1);
        Observer.Instance.Announce(new Message(EventType.ChangePlayerState, EPlayerState.Flash));

    }

    protected override bool DoSkill()
    {
        counter++;
        return counter <= maxCounter;
    }

    protected override void OnSkillFinish()
    {
        PipePoolManager.Instance.ChangeSpeedGroup(GameConstants.NORMAL_PIPE_SPEED);
        counter = 0;
    }
}
