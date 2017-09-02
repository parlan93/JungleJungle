using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SwipeDirection
{
    NONE = 0,
    UP = 1,
    DOWN = 2
}

public class SwipeManager : MonoBehaviour
{
    private static SwipeManager instance;
    public static SwipeManager Instance { get { return instance; } }

    public SwipeDirection Direction { get; set; }
    private Vector3 touchPosition;
    private float swipeResistance = 50.0f;

    private void Start()
    {
        instance = this;
    }

    private void Update()
    {
        Direction = SwipeDirection.NONE;

        if (Input.GetMouseButtonDown(0))
        {
            touchPosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            Vector2 deltaSwipe = touchPosition - Input.mousePosition;

            if (Mathf.Abs(deltaSwipe.y) > swipeResistance)
            {
                Direction |= (deltaSwipe.y < 0) ? SwipeDirection.UP : SwipeDirection.DOWN;
            }
        }
    }

    public bool IsSwiping(SwipeDirection dir)
    {
        return (Direction & dir) == dir;
    }
}

