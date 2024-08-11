using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PipeState
{
    Normal,
    Broken,
}
public class Pipe : MonoBehaviour
{
    private PipeState state = PipeState.Normal;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            object state = Observer.Instance.GetCallBack(new Message(EventType.GetState));
            if (state is EPlayerState)
            {
                if (state is not EPlayerState.Normal)
                {
                    BreakPipe();
                }
                else
                {
                    // GameManager.Instance.EndGame();
                }
            }
        }
    }

    private void BreakPipe()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.red;
            state = PipeState.Broken;
        }
    }
    public void ToNormalPipe()
    {
        if (state != PipeState.Normal)
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.color = Color.white;
            }
            state = PipeState.Normal;
        }
    }
}
