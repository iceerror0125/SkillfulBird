using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;
using UnityEngine.UIElements;

public class ParralelBackgroundManager : MonoBehaviour
{
    [SerializeField] private List<ParallelBackground> backgrounds;

    private void OnEnable()
    {
        Observer.Instance.Subscribe(EventType.BackgroundReachLimit, RespawnBackground);
    }
    private void OnDisable()
    {
        Observer.Instance.UnSubscribe(EventType.BackgroundReachLimit, RespawnBackground);
    }

    private void Start()
    {
        InitBackground();
    }
    private void InitBackground()
    {
        float width = backgrounds[0].GetWidth();
        int counter = -1;
        float distance = width / 2;
        foreach (var background in backgrounds)
        {
            background.transform.position = new Vector2 (distance * counter, 0);
            background.GenerateBackground();
            counter++;
        }
    }
    private void RespawnBackground(Message msg)
    {
        ParallelBackground bg = (ParallelBackground)msg.param;
        float x = GetRespawnPosition();
        float width = backgrounds[0].GetWidth();
        float distance = width / 2;
        bg.transform.position = new Vector2(x + distance, 0);
        bg.GenerateBackground();
    }
    private float GetRespawnPosition()
    {
        float max = 0;
        foreach (var background in backgrounds)
        {
            float x = background.transform.position.x;
            if (x > max)
                max = x;
        }
        return max;
    }
}
