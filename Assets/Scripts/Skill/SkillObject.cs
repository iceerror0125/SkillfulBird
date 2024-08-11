using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillObject : MonoBehaviour
{
    private SpriteRenderer image;
    private SkillData data;
    private Skill skill;

    public Action onSkillObjectTrigger;

    private void Awake()
    {
        image = GetComponentInChildren<SpriteRenderer>();
    }
    public void SetData(SkillData data, Skill skill)
    {
        this.data = data;
        image.sprite = data.sprite;
        image.color = data.color;
        this.skill = skill;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            // back to pool
            gameObject.SetActive(false);
            gameObject.transform.SetParent(SkillObjectPoolManager.Instance.transform, false);

            // plus point
            onSkillObjectTrigger?.Invoke();

            // Set skill
            SkillManager skillManager = SkillManager.Instance;
            var mapping = skillManager.CharacterMapping;

            if (mapping.ContainsKey(data.id))
            {
                skillManager.SetCurrentSkill(skill);

                // active immediately if skill is passive
                if (data.type is SkillType.PASSIVE)
                {
                    skillManager.ActivateSkill();
                }
            }
        }
    }
}
