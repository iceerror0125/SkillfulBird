using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float jumpForce = 5;
    public Rigidbody2D rb { get; private set; }
    private Tween tween;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Jump()
    {
        rb.velocity = Vector3.zero;
        rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
    }

    public void FallDown()
    {
        tween.Kill();
        tween = transform.DORotate(new Vector3(0, 0, -20), 0.2f);
    }
    public void FallUp()
    {
        tween.Kill();
        tween = transform.DORotate(new Vector3(0, 0, 20), 0.2f);
    }
    public void ChangeToNoRotation()
    {
        if (transform.eulerAngles.z != 0)
        {
            tween.Kill();
            tween = transform.DORotate(Vector2.zero, 0.2f);
        }
    }

    public void ChangeToNoGravity()
    {
        if (rb.gravityScale != 0)
            rb.gravityScale = 0;
    }
    public void ChangeToDefaultGravity()
    {
        rb.gravityScale = GameConstants.DEFAUT_GRAVITY;
    }
    public void TurnOffYVelocity()
    {
        rb.velocity = new Vector2 (rb.velocity.x, 0);
    }
    public void ChangeToZeroVelocity()
    {
        rb.velocity = Vector2.zero; 
    }
    public void MoveYAxis(float y)
    {
        rb.velocity = new Vector2(rb.velocity.x, y);
    }
    public void Immobilize()
    {
        ChangeToZeroVelocity();
        ChangeToNoGravity();
    }
    public void Mobilize()
    {
        ChangeToDefaultGravity();
    }
}
