using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pet : MonoBehaviour
{
    [SerializeField] private Vector2 startPoint;
    [SerializeField] private Vector2 endPoint;
    [SerializeField] private Vector2 middlePoint;
    private float interpolateAmount;

    private Vector2 AB;
    private Vector2 BC;
    private Vector2 ABC;

    [ContextMenu("To Player Position")]
    private void ToPlayerPosition()
    {
        startPoint = PlayerManager.Instance.Player.transform.position;
    }
    [ContextMenu("Calculate middle point")]
    private void CalculateMiddlePoint()
    {
        CalculateMiddlePointValue();
    }

    public void SetData(Vector2 startPoint, Vector2 endPoint)
    {
        transform.position = startPoint;
        middlePoint = new Vector2(startPoint.x - 20, startPoint.y + 3);
        gameObject.SetActive(true);
    }

    private void Update()
    {
        if (gameObject.activeSelf)
        {
            interpolateAmount = (interpolateAmount + Time.deltaTime) % 1.0f;
            MoveCurve();
        }
       
    }

    private void MoveCurve()
    {
        AB = Vector2.Lerp(startPoint, endPoint, interpolateAmount);
        BC = Vector2.Lerp(middlePoint, endPoint, interpolateAmount);
        ABC = Vector2.Lerp(AB, BC, interpolateAmount);
        transform.position = ABC;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       /* if (collision != null && collision.tag.Equals("Pipe"))
        {
            gameObject.SetActive(false);
            collision.GetComponent<Pipe>().BreakPipe();
        }*/
    }

    private void CalculateMiddlePointValue()
    {
    }
}
