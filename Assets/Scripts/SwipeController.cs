using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeController : MonoBehaviour
{
    public delegate void Slide(Vector2 direction);
    public static event Slide SwipeEveant;
    private Vector2 StartPoint;
    private Vector2 EndPoint;
    [SerializeField] private bool onType;
    [SerializeField] public int Deadzone = 80;

    [SerializeField] private bool isMobile;


    void Update()
    {
        if (!isMobile)
        {
            if (Input.GetMouseButtonDown(0))
            {
                onType = true;
                StartPoint = Input.mousePosition;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                CleanSwipes();
            }
        }
        else
        {
            if (Input.touchCount > 0)
            {
                onType = true;
                StartPoint = Input.GetTouch(0).position;
            }
            else if (Input.GetTouch(0).phase == 0)
            {
                CleanSwipes();
            }
        }

        CheckSwipe();
    }

    private void CleanSwipes()
    {
        onType = false;
        StartPoint = Vector2.zero;
        EndPoint = Vector2.zero;
    }

    private void CheckSwipe()
    {
        EndPoint = Vector2.zero;

        if (onType)
        {
            if (!isMobile && Input.GetMouseButton(0))
            {
                EndPoint = (Vector2)Input.mousePosition - StartPoint;
            }
            else if (Input.touchCount > 0)
            {
                EndPoint = Input.GetTouch(0).position - StartPoint;
            }
        }
        if (EndPoint.magnitude > Deadzone)
        {
            if (SwipeEveant != null)
            {
                if (Mathf.Abs(EndPoint.y) > Mathf.Abs(EndPoint.x))
                {
                    SwipeEveant?.Invoke(EndPoint.y > 0 ? Vector2.up : Vector2.down);
                }
            }
            CleanSwipes();
        }

    }
}
