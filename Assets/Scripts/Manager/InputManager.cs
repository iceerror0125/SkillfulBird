using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class InputManager : SingletonMono<InputManager>
{

    #region Mouse Setting

    private bool isMouseUp;
    private float priviousMouseYPos;
    public float mouseVelocity { get; private set; }
    private float currentMouseYPosition;
    private float firstClickTimer;
    #endregion

    public event Action fixedUpdateCallBack;
    public event Action doubleClickCallBack;

    private void FixedUpdate()
    {
        fixedUpdateCallBack?.Invoke();
    }
    private void Update()
    {
        DragMouse();
    }

    private void DragMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            priviousMouseYPos = Input.mousePosition.y;

            //check double Click
            if (Time.time - firstClickTimer < 0.3f) {
                Debug.Log("Double Click");
                firstClickTimer = 0;
                doubleClickCallBack?.Invoke();
            }
            else
            {
                firstClickTimer = Time.time;
            }
        }

        if (Input.GetMouseButton(0))
        {
            currentMouseYPosition = Input.mousePosition.y;
            if (priviousMouseYPos > currentMouseYPosition)
            {
                Debug.Log("Mouse down");
            }
            else if (priviousMouseYPos < currentMouseYPosition)
            {
                Debug.Log("Mouse up");
            }
            mouseVelocity = currentMouseYPosition - priviousMouseYPos;
            priviousMouseYPos = currentMouseYPosition;
        }
    }

    private void OnMouseDrag()
    {

    }
}
