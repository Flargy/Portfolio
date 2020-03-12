using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author: Hjalmar Andersson

public static class HelpClass
{
    public static Vector2 NormalizeForce2D(Vector2 speed, Vector2 normal)
    {
        Vector2 projection;
        if (Vector2.Dot(speed, normal) > 0)
        {
            return projection = 0 * normal;
        }
        projection = Vector2.Dot(speed, normal) * normal;
        return -projection;
    }

    /// <summary>
    /// Calculates the normal force of a velocity and the normal of the collision.
    /// </summary>
    /// <param name="speed"> A velocity Vector3</param>
    /// <param name="normal"> The normal of the surface the Velocity will collide with</param>
    /// <returns></returns>
    public static Vector3 NormalizeForce(Vector3 speed, Vector3 normal)
    {
        Vector3 projection;
        if (Vector3.Dot(speed, normal) > 0)
        {
            return projection = 0 * normal;
        }
        projection = Vector3.Dot(speed, normal) * normal;
        return -projection;
    }
    //notused
    public static Vector2 AntiCollision(Vector2 speed, Vector2 normal)
    {
        Vector2 projection;
        if (Vector2.Dot(speed, normal) > 0)
        {
            return projection = 0 * normal;
        }
        projection = Vector2.Dot(speed, normal) * normal;
        return -projection;
    }

    public static Vector2 PointOnRectangle(Vector2 dirr, Vector2 size)
    {
        Vector2 halfSize = size * 0.5f;
        float radian = Mathf.Atan2(dirr.y, dirr.x);
        float distanceX = Mathf.Abs(halfSize.x / Mathf.Cos(radian));
        float distanceY = Mathf.Abs(halfSize.y / Mathf.Sin(radian));
        return dirr.normalized * Mathf.Min(distanceY, distanceX);
    }

    public static Vector2 RotateVector(Vector2 vector, float angle)
    {
        if (vector.magnitude < Mathf.Epsilon)
            return Vector2.zero;
        float radian = Mathf.Atan2(vector.y, vector.x) + (angle * Mathf.Deg2Rad);
        return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian)) * vector.magnitude;
    }
}
