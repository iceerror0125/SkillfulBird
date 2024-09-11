using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill
{
    private int charmId;
    public int CharmId => charmId;
    private bool beingActiveSkill;

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
            FinishSkill();
            SkillManager.Instance.SetCurrentSkill(null);
            Observer.Instance.Announce(new Message(EventType.ChangePlayerState, EPlayerState.Normal));
        }
    }

    /// <summary>
    /// called when player collect skill object
    /// </summary>
    public void ActivateSkill()
    {
        if (beingActiveSkill)
            return;

        beingActiveSkill = true;
        OnActivateSkill();
    }

    protected abstract void OnActivateSkill();

    protected abstract bool DoSkill();

    private void FinishSkill()
    {
        beingActiveSkill = false;
        OnSkillFinish();
    }
    protected abstract void OnSkillFinish();
}
