using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeGroup : MonoBehaviour
{
    [SerializeField] private float speed = GameConstants.DEFAULT_PIPE_SPEED;
    [SerializeField] private Pipe upperPipe;
    [SerializeField] private Pipe lowerPipe;
    [SerializeField] private PointTrigger pointTrigger;

    public float yUpperPipeBackup { get; private set; }
    public float yLowerPipeBackup { get; private set; }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }
    private void Update()
    {
        transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
    }
    public Pipe GetUpperPipe()
    {
        return upperPipe;
    }
    public Pipe GetLowerPipe()
    {
        return lowerPipe;
    }
    public PointTrigger GetPointTrigger()
    {
        return pointTrigger;
    }

    public void MoveBothPipeInSameYAxis(float value)
    {
        upperPipe.transform.DOMoveY(value, 0.5f);
        lowerPipe.transform.DOMoveY(-value, 0.5f);
    }
    /// <summary>
    /// Back to back up value
    /// </summary>
    public void BackToNormalYAxis()
    {
        float originY = pointTrigger.transform.localPosition.y;
        upperPipe.transform.DOLocalMoveY(originY + 1, 0.5f);
        lowerPipe.transform.DOLocalMoveY(originY - 1, 0.5f);
    }
    public void ResetState()
    {
        upperPipe.ToNormalPipe();
        lowerPipe.ToNormalPipe();
        SkillObject skillObject = GetComponentInChildren<SkillObject>();
        if (skillObject != null)
        {
            skillObject.BackToPool();
        }
    }
}
