using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill
{
    private int charmId;
    public int CharmId => charmId;

    public Skill(int charmId)
    {
        this.charmId = charmId;
    }

    /// <summary>
    /// called when player collide with point trigger or pipe
    /// </summary>
    public void TriggerSkill()
    {
        if (!DoSkill())
        {
            OnSkillFinish();
            SkillManager.Instance.SetCurrentSkill(null);
            Observer.Instance.Announce(new Message(EventType.ChangePlayerState, EPlayerState.Normal));
        }
    }

    /// <summary>
    /// called when player collect skill object
    /// </summary>
    public abstract void ActivateSkill();

    protected abstract bool DoSkill();
    protected abstract void OnSkillFinish();
}
