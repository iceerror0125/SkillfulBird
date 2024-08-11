using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitRange : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
           
        if (collision.tag == "Pipe")
        {
            PipePoolManager.Instance.BackToInitPostion();
            SkillManager.Instance.CheckSpawnCharm();
        }
    }
}
