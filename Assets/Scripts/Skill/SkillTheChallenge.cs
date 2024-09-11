using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTheChallenge : Skill
{
    int counter = 0;
    int maxcounter = 10;
    float speedChanged = 8f;
    public SkillTheChallenge(int charmId) : base(charmId)
    {
    }

    protected override void OnActivateSkill()
    {
        PipePoolManager.Instance.ChangeSpeedGroup(speedChanged);
    }

    protected override bool DoSkill()
    {
        GameManager.Instance.PlusPoint();
        counter++;
        return counter <= maxcounter;
    }

    protected override void OnSkillFinish()
    {
        PipePoolManager.Instance.ChangeSpeedGroup(GameConstants.DEFAULT_PIPE_SPEED);
        counter = 0;
    }
}
