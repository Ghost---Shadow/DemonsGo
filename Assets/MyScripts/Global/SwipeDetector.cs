﻿using UnityEngine;
using System.Collections;

public class SwipeDetector : MonoBehaviour {

	public enum SwipeDirection{
         Up,
         Down,
         Right,
         Left
     }

     public delegate void SwipeAction(SwipeDirection direction);
     public static event SwipeAction Swipe;
     private bool swiping = false;
     private bool eventSent = false;
     private Vector2 lastPosition;

     void Update () {
         if (Input.touchCount == 0)
             return;

         if (Input.GetTouch(0).deltaPosition.sqrMagnitude != 0){
             if (swiping == false){
                 swiping = true;
                 lastPosition = Input.GetTouch(0).position;
                 return;
             }
             else{
                 if (!eventSent) {
                     if (Swipe != null) {
                         Vector2 direction = Input.GetTouch(0).position - lastPosition;
                         if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y)){
                             if (direction.x > 0)
                                 Swipe(SwipeDirection.Right);
                             else
                                 Swipe(SwipeDirection.Left);
                         }
                         else{
                             if (direction.y > 0)
                                 Swipe(SwipeDirection.Up);
                             else
                                 Swipe(SwipeDirection.Down);
                         }

                         eventSent = true;
                     }
                 }
             }
         }
         else{
             swiping = false;
             eventSent = false;
         }
     }
}