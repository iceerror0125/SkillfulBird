using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PipePoolManager : SingletonMono<PipePoolManager>
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int pipeGroupQuantity = 5;
    [SerializeField] private float respawnPosition = 10;
    [Header("Control Pipe group")]
    [Range(1, 2.5f)]
    [SerializeField] private float yRange = 2.5f; // to adjust the height of pipe group
    [SerializeField] private float initPosition = 0; // position of first pipe group
    [SerializeField] private int respawnSpace = 5; // space between pipe groups

    private Queue<GameObject> pools = new Queue<GameObject>();

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        for (int i = 0; i < pipeGroupQuantity; i++)
        {
            GameObject pipe = Instantiate(prefab);
            pipe.name = "item_" + i;
            pipe.transform.position = new Vector2(transform.position.x + initPosition, UnityEngine.Random.Range(-yRange, yRange));
            pools.Enqueue(pipe);
            initPosition += respawnSpace;
            SkillManager.Instance.CheckSpawnCharm();
        }
        prefab.SetActive(false);
    }
    public void BackToInitPostion()
    {
        GameObject go = pools.Dequeue();
        respawnPosition = pools.LastOrDefault().transform.position.x + respawnSpace;
        go.transform.position = new Vector2(respawnPosition, UnityEngine.Random.Range(-yRange, yRange));

        Pipe[] pipes = go.GetComponentsInChildren<Pipe>();
        foreach (Pipe pipe in pipes)
        {
            pipe.ToNormalPipe();
        }

        pools.Enqueue(go);
    }
    public GameObject GetHeadPipe()
    {
        return pools.LastOrDefault();
    }
    public void ChangeSpeedGroup(float speed)
    {
        foreach(var obj in pools)
        {
            PipeGroup group = obj.GetComponent<PipeGroup>();
            if (group != null)
            {
                group.SetSpeed(speed);
            }
        }
    }
    public void SpawnSpecificSkill()
    {

    }
}
