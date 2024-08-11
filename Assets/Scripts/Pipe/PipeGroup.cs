using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeGroup : MonoBehaviour
{
    [SerializeField] private float speed = GameConstants.NORMAL_PIPE_SPEED;
    // data of 2 pipe
    // data of trigger point
    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }
    private void Update()
    {
        transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
    }
}
