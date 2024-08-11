using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkillManager : SingletonMono<SkillManager>
{
    [SerializeField] private SkillAsset charmData;
    // private SkillManager skillManager;
    private static Dictionary<int, Skill> charmMapping;
    private Skill currentSkill;

    #region Getter Setter
    public Dictionary<int, Skill> CharacterMapping => charmMapping;
    public Skill CurrentSkill => currentSkill;
    public void SetCurrentSkill(Skill skill)
    {
        currentSkill = skill;
    }
    #endregion


    private void Start()
    {
        MapCharmAndSkill(charmData.dataList, GetSkillList());
    }

    private static List<Skill> GetSkillList()
    {
        return new List<Skill>() {
            new Skill1(1),
            new Skill2(2),
            new Skill3(3),
            new Skill4(4),
        };
    }

    private void MapCharmAndSkill(List<SkillData> charmList, List<Skill> skillList)
    {
        if (charmMapping == null)
        {
            charmMapping = new Dictionary<int, Skill>();

            // set charm id as a key
            foreach (SkillData charm in charmList)
            {
                charmMapping.Add(charm.id, null);
            }

            // set Skill as a value
            foreach (Skill skill in skillList)
            {
                if (charmMapping.ContainsKey(skill.CharmId))
                {
                    charmMapping[skill.CharmId] = skill;
                }
            }
        }
    }

    public void CheckSpawnCharm()
    {
        int random = Random.Range(0, 100);
        int rate = 50;
        if (random < rate)
        {
            SpawnCharm();
        }
    }
    private void SpawnCharm()
    {
        GameObject pipe = PipePoolManager.Instance.GetHeadPipe();
        SkillObject charm = SkillObjectPoolManager.Instance.GetCharmObject();
        int min = 1;
        int max = 1;// charmData.dataList.Count;
        int index = Random.Range(min, max);
        if (charm != null)
        {
            charm.transform.SetParent(pipe.transform);
            charm.transform.position = pipe.transform.position;

            if (charmMapping.TryGetValue(charmData.dataList[index].id, out var skill))
            {
                charm.SetData(charmData.dataList[index], skill);
            }
        }
    }
    public void ActivateSkill()
    {
        if (currentSkill != null)
        {
            currentSkill.ActivateSkill();
        }
    }
    public void TriggerSkill()
    {
        if (currentSkill != null)
        {
            currentSkill.TriggerSkill();
        }
    }
}
