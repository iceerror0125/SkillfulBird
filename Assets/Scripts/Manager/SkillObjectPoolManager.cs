using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillObjectPoolManager : SingletonMono<SkillObjectPoolManager>
{
    [SerializeField] private List<SkillObject> listCharm;

    private Queue<SkillObject> queue;

    private void Start()
    {
        if (listCharm != null && listCharm.Count > 0)
        {
            queue = new Queue<SkillObject>();
            foreach (SkillObject obj in listCharm)
            {
                queue.Enqueue(obj);
            }
        }
    }

    public SkillObject GetCharmObject()
    {
        if (queue.Count < 0)
        {
            return null;
        }

        SkillObject returnObj = queue.Dequeue();
        if (returnObj != null)
        {
            returnObj.gameObject.SetActive(true);
            queue.Enqueue(returnObj);
            return returnObj;
        }

        return null;

    }
}
