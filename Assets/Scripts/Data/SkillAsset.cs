using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill Data List", menuName = "Skill/SkillList")]
public class SkillAsset : ScriptableObject
{
    public List<SkillData> dataList;
}

[Serializable]
public class SkillData
{
    public int id;
    public Sprite sprite;
    public Color color = Color.white;
    public SkillType type;
    public string skillName;
    public string description;
}
public enum SkillType
{
    ACTIVE,
    PASSIVE
}