using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillImmortal : Skill
{
    private int counter = 0;
    private int maxCounter = 3;
    public SkillImmortal(int charmId) : base(charmId)
    {
    }

    protected override void OnActivateSkill()
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
