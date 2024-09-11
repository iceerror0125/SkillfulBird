using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : SingletonMono<SkillManager>
{
    [SerializeField] private SkillAsset charmData;
    [SerializeField] private GameObject pet;
    [SerializeField] private GameObject directionLine;
    [SerializeField] private InputManager inputManager;

    public GameObject Pet => pet;
    public GameObject DirectionLine => directionLine;

    private static Dictionary<int, Skill> charmMapping;
    private Skill currentSkill;
    private int spawnAtScore;

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
            new SkillRocket(1),
            new SkillImmortal(2),
            new SkillPetSummon(3),
            new SkillTheChallenge(4),
            new SkillOpenWay(5),
            // new SkillWearWeights(6),
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

    public void CheckSpawnSkillObject()
    {
        if (GameConstants.SKILL_SPAWNED)
            return;

        int random = UnityEngine.Random.Range(0, 100);
        int rate = GameConstants.SKILL_SPAWN_RATE;
        bool isReachScoreSpawn = PipePoolManager.Instance.PipeCount - spawnAtScore == GameConstants.SKILL_SPAWN_AFTER_PIPES;
        Debug.Log($"{PipePoolManager.Instance.PipeCount} - {spawnAtScore}");
        // spawn skill object
        if (random < rate && isReachScoreSpawn)
        {
            GameConstants.SKILL_SPAWNED = true;
            spawnAtScore = 0;
            SpawnSkillObject();
        }
    }
    private void SpawnSkillObject()
    {
        GameObject pipe = PipePoolManager.Instance.GetHeadPipe();
        SkillObject charm = SkillObjectPoolManager.Instance.GetCharmObject();
        int min = 1;
        int max = charmData.dataList.Count;
        int index = UnityEngine.Random.Range(4, 4);
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
    public void ShowDirectionLine()
    {
        directionLine.SetActive(true);
        inputManager.fixedUpdateCallBack += OnChangeDirectionLineAngle;
    }
    public void HideDirectionLine()
    {
        directionLine.SetActive(false);
        inputManager.fixedUpdateCallBack -= OnChangeDirectionLineAngle;
    }

    public float offsetSpeedValue = 5;
    private void OnChangeDirectionLineAngle()
    {
        float angleOffset = inputManager.mouseVelocity * offsetSpeedValue;
        float newZAngle = directionLine.transform.localEulerAngles.z + angleOffset;
        float limitAngle = 45;
        
        var vectorAngle = new Vector3(0, 0, ClampAngle(newZAngle, angleOffset, limitAngle));
        directionLine.transform.DORotate(vectorAngle, 0.1f);
        // directionLine.transform.eulerAngles = vectorAngle;
    }
    private float ClampAngle(float newAngle, float dir, float limitValue)
    {
        if (dir > 0)
        {
            if (newAngle > limitValue && newAngle < 360 - limitValue)
            {
                return limitValue;
            }
        }
        else
        {
            if (newAngle < 360 - limitValue && newAngle > limitValue)
                return 360 - limitValue;
        }
        return newAngle;
    }
}
