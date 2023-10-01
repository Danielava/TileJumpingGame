
using UnityEngine;

public static class DirectionExtensions
{
    public static Vector2 ToVector2(this Direction direction)
    {
        switch (direction)
        {
            case Direction.Down:
                return Vector2.down;
            case Direction.Up:
                return Vector2.up;
            case Direction.Left:
                return Vector2.left;
            case Direction.Right:
                return Vector2.right;
            default:
                return Vector2.right;
        }
    }
}