using UnityEngine;
using System.Collections;

public class SwipeDetector : MonoBehaviour
{

    public enum SwipeDirection
    {
        Up,
        Down,
        Right,
        Left
    }

    public float minimumSwipeLength = 20f;

    public delegate void SwipeAction(SwipeDirection direction);
    public static event SwipeAction Swipe;

    private bool swiping = false;
    private bool eventSent = false;
    private Vector2 lastPosition = Vector2.zero;
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (lastPosition == Vector2.zero)
            {
                lastPosition = Input.mousePosition;
                return;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {            
            Vector2 direction = (Vector2)Input.mousePosition - lastPosition;
            if (direction.sqrMagnitude < minimumSwipeLength){
                return;
            }
            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            {
                if (direction.x > 0)
                    Swipe(SwipeDirection.Right);
                else
                    Swipe(SwipeDirection.Left);
            }
            else
            {
                if (direction.y > 0)
                    Swipe(SwipeDirection.Up);
                else
                    Swipe(SwipeDirection.Down);
            }
            lastPosition = Vector2.zero;
        }
    }
}
