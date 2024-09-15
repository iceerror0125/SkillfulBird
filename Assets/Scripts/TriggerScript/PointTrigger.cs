using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointTrigger : MonoBehaviour
{
    private BoxCollider2D col;
    private void Start()
    {
        col = GetComponent<BoxCollider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            PipePoolManager poolManager = PipePoolManager.Instance;

            GameManager.Instance.PlusPoint();
            SkillManager.Instance.TriggerSkill();
            poolManager.ChangeNextGroupIndex(true);
            poolManager.GetNextPipeGroup();

        }
    }

    public void SetTrigger(bool isTrigger)
    {
        col.isTrigger = isTrigger;
    }
}
