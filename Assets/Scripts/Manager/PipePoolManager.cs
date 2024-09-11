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
    private int nextGroupIndex;
    private int pipeCount;
    public int PipeCount => pipeCount;

    private void OnEnable()
    {
        Observer.Instance.Subscribe(EventType.PlusPoint, PlusCount);
    }
    private void OnDisable()
    {
        Observer.Instance.UnSubscribe(EventType.PlusPoint, PlusCount);
    }
    private void PlusCount(Message msg)
    {
        pipeCount++;
    }
    public void ResetPipeCount()
    {
        pipeCount = 0;
    }

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
            // SkillManager.Instance.CheckSpawnSkillObject();
        }
        prefab.SetActive(false);
        nextGroupIndex = 0;
        pipeCount = 0;
    }
    public void BackToInitPostion()
    {
        GameObject go = pools.Dequeue();
        respawnPosition = pools.LastOrDefault().transform.position.x + respawnSpace;
        go.transform.position = new Vector2(respawnPosition, UnityEngine.Random.Range(-yRange, yRange));

        PipeGroup group = go.GetComponent<PipeGroup>();
        group.ResetState();
      /*  Pipe[] pipes = go.GetComponentsInChildren<Pipe>();
        foreach (Pipe pipe in pipes)
        {
            pipe.ToNormalPipe();
        }*/
        

        pools.Enqueue(go);
    }
    public GameObject GetHeadPipe()
    {
        return pools.LastOrDefault();
    }
    public void ChangeSpeedGroup(float speed)
    {
        if (speed > 0)
        {
            speed *= -1;
        }
        foreach (var obj in pools)
        {
            PipeGroup group = obj.GetComponent<PipeGroup>();
            if (group != null)
            {
                group.SetSpeed(speed);
            }
        }
    }

    /// <summary>
    /// Get the group in front of the player
    /// </summary>
    public PipeGroup GetNextPipeGroup()
    {
        if (nextGroupIndex >= pools.Count)
        {
            return null;
        }

        List<GameObject> temp = pools.ToList();
        PipeGroup group = temp[nextGroupIndex].GetComponent<PipeGroup>();
        return group;
    }
    public void ChangeNextGroupIndex(bool isIncrease)
    {
        if (isIncrease)
        {
            nextGroupIndex++;
        }
        else
        {
            nextGroupIndex--;
        }
    }
    public void OpenPipeGroup()
    {
        foreach (GameObject group in pools)
        {
            PipeGroup pipeGroup = group.GetComponent<PipeGroup>();
            float yAxis = 4;
            pipeGroup.MoveBothPipeInSameYAxis(yAxis);
        }
    }
    public void ClosePipeGroup()
    {
        foreach(GameObject group in pools)
        {
            PipeGroup pipeGroup = group.GetComponent<PipeGroup>();
            pipeGroup.BackToNormalYAxis();
        }
    }
    public void StopPipes()
    {
        foreach(GameObject obj in pools)
        {
            PipeGroup group = obj.GetComponent<PipeGroup>();
            group.SetSpeed(0);
        }
    } 
    public void SetSpeedPipeByDefault()
    {
        foreach (GameObject obj in pools)
        {
            PipeGroup group = obj.GetComponent<PipeGroup>();
            group.SetSpeed(GameConstants.DEFAULT_PIPE_SPEED);
        }
    }
}
