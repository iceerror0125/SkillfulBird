using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkillPetSummon : Skill
{
    private Pet pet;
    public SkillPetSummon(int charmId) : base(charmId)
    {
    }

    protected override void OnActivateSkill()
    {
        pet = SkillManager.Instance.Pet.GetComponent<Pet>();
        DoSkill();
    }

    protected override bool DoSkill()
    {
        PipeGroup nextGroupPipe = PipePoolManager.Instance.GetNextPipeGroup();
        int highValue = GetUpOrDownFlag();
        int yTargetValue = GetYTargetPosition();
        Pipe pipe = highValue > 0 ? nextGroupPipe.GetUpperPipe() : nextGroupPipe.GetLowerPipe();

        Vector2 target = new Vector2(pipe.transform.position.x, yTargetValue * highValue);
        pet.SetData(PlayerManager.Instance.Player.transform.position, target);
        return false;
    }

    protected override void OnSkillFinish()
    {

    }

    private int GetUpOrDownFlag()
    {
        int random = Random.Range(0, 2);
        if (random == 0)
            return 1;
        return -1;
    }
    private int GetYTargetPosition()
    {
        return Random.Range(1, 5);
    }
    private void CalculateCurve()
    {

    }
}
