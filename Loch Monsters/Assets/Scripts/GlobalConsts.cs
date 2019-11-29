using UnityEngine;
using System.Collections;

public static class GlobalConsts
{
    public const int DEFAULT_LENGTH = 3;
    public static Vector2 FIELD_SIZE = new Vector2(25, 25);
    public static float SNAKE_SPEED = 5;
    public static float SEGMENT_DIST = 1;


    public static Vector2 ClampToRange(Vector2 val, Vector2 range)
    {
        Vector2 clamped = val;
        if (val.x < -range.x)
        {
            clamped.x = -range.x;
        }
        else if (val.x > range.x)
        {
            clamped.x = range.x;
        }

        if (val.y < -range.y)
        {
            clamped.y = -range.y;
        }
        else if (val.y > range.y)
        {
            clamped.y = range.y;
        }


        return clamped;
    }

}
