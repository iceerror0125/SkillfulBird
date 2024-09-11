using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillOpenWay : Skill
{
    int counter = 0;
    int maxCounter = 3;
    public SkillOpenWay(int charmId) : base(charmId)
    {
    }

    protected override void OnActivateSkill()
    {
        PipePoolManager.Instance.OpenPipeGroup();
    }

    protected override bool DoSkill()
    {
        counter++;
        return counter <= maxCounter;
    }

    protected override void OnSkillFinish()
    {
        counter = 0;
        PipePoolManager.Instance.ClosePipeGroup();
    }
}
