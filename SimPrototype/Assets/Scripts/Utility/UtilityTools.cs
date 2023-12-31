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
        
        //adapted from: https://stackoverflow.com/questions/13169393/extract-number-at-end-of-string-in-c-sharp
        static public int GetLastNumberFromString(string str)
        {
            if (string.IsNullOrEmpty(str)) return -1;
            
            int numberOfDigitsAtEnd = 0;
            for (var i = str.Length - 1; i >= 0; i--)
            {
                if (!char.IsNumber(str[i]))
                {
                    break;
                }

                numberOfDigitsAtEnd++;
            }
        
            var result = str[^numberOfDigitsAtEnd..];
            
            return int.TryParse(result, out int resultAsInt) ? resultAsInt : -1;

        }

        static public string GetStringWithoutLastNumber(string str)
        {
            var digits = new[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            var input = str;
            var result = input.TrimEnd(digits);
            return result;
        }
    }
}

