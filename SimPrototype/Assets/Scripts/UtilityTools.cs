using System.Collections;
using System.Collections.Generic;
using SimPrototype;
using UnityEngine;

namespace UtilityTools
{
    public static class UtilityTools
    {
        static public Enums.Direction GetDirectionFromVector2(Vector2 direction)
        {
            direction = direction.normalized;
            // if  right is pressed, x will be positive. Y should be 0 then but if using analog stick maybe player is moving diagonally
            // for simplicity we use only for directions. So we should consider the direction with most "power"
            // then in a case where player is moving right (x > 0) but also moving up (y > 0)
            // we should consider right only if the analog is more tilted to the right then upwards
            if (direction.x > 0 && Mathf.Abs(direction.y) < Mathf.Abs(direction.x))
            {
                return Enums.Direction.Right;
            }
            else if (direction.x < 0 && Mathf.Abs(direction.y) < Mathf.Abs(direction.x))
            {
                return Enums.Direction.Left;
            } 
            else if (direction.y > 0 && Mathf.Abs(direction.y) > Mathf.Abs(direction.x))
            {
                return Enums.Direction.Up;
            }
            else
            {
                // defaults to down since it's the "idle" frame
                return Enums.Direction.Down;
            }
        }
    }
}

