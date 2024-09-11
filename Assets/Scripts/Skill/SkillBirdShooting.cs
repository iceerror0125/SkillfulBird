using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBirdShooting : Skill
{
    private bool isCancelSkill = true;
    public SkillBirdShooting(int charmId) : base(charmId)
    {
    }

    protected override void OnActivateSkill()
    {
        PlayerManager.Instance.Player.Immobilize();
        PipePoolManager.Instance.StopPipes();
        SkillManager.Instance.ShowDirectionLine();
        InputManager.Instance.doubleClickCallBack += TriggerSkill;
        Debug.Log("Activate");
    }

    protected override bool DoSkill()
    {
        PipePoolManager.Instance.ChangeSpeedGroup(20);
        if (isCancelSkill) {
            GameManager.Instance.StartCoroutine(CancelSkill());
        }
        return isCancelSkill;
    }
    private IEnumerator CancelSkill()
    {
        yield return new WaitForSeconds(2);
        isCancelSkill = false;
        TriggerSkill();
    }

    protected override void OnSkillFinish()
    {
        PlayerManager.Instance.Player.Immobilize();
        PipePoolManager.Instance.SetSpeedPipeByDefault();
        SkillManager.Instance.HideDirectionLine();
        InputManager.Instance.doubleClickCallBack -= TriggerSkill;
        isCancelSkill = true;
    }
}
