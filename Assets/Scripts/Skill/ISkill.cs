using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkill
{

    public int charmId { get; set; }

    public void Activate();
}
