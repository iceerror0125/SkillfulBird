using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill2 : Skill
{
    private int counter = 0;
    private int maxCounter = 3;
    public Skill2(int charmId) : base(charmId)
    {
    }

    public override void ActivateSkill()
    {
        Observer.Instance.Announce(new Message(EventType.ChangePlayerState, EPlayerState.Immortal));
    }

    protected override bool DoSkill()
    {
        counter++;
        return counter <= maxCounter;
    }

    protected override void OnSkillFinish()
    {
        counter = 0;
    }
}
