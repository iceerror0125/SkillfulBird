using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillRocket : Skill
{
    private float speedUp = -20;
    private float originSpeed;
    private float maxCounter = 3;
    private float counter = 0;
    public SkillRocket(int charmId) : base(charmId)
    {
    }

    protected override void OnActivateSkill()
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
        PipePoolManager.Instance.ChangeSpeedGroup(GameConstants.DEFAULT_PIPE_SPEED);
        counter = 0;
    }
}
