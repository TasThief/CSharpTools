using System;
using UnityEngine;
/// <summary>
/// The four direction enumeration
/// </summary>
public enum Direction
{
    north,
    east,
    south,
    west,
    nothing,
    northeast,
    southeast,
    southwest,
    northwest,
}

public static class DirectionExtentions
{
    public static Direction Perpendicular(this Direction dir)
    {
        switch (dir) {
            case Direction.west:
                dir = Direction.north;
                break;
            case Direction.nothing:
                dir = Direction.nothing;
                break;
            case Direction.northwest:
                dir = Direction.northeast;
                break;
            default:
                dir = (Direction)(((int)dir) + 1);
                break;
        }
        return dir;
    } 

    public static Vector3 angle(this Direction dir) {
        return new Vector3(0.0f, (int)dir * 90.0f - 90.0f, 0.0f);
    }
    public static float ToAngle45(this Direction dir)
    {
        float result = 0.0f; 
        switch (dir)
        {
            case Direction.north:
                result = 0.0f;
                break;
            case Direction.northeast:
                result = 45.0f;
                break;
            case Direction.east:
                result = 90.0f;
                break;
            case Direction.southeast:
                result = 135.0f;
                break;
            case Direction.south:
                result = 180.0f;
                break;
            case Direction.southwest:
                result = 225.0f;
                break;
            case Direction.west:
                result = 270.0f;
                break;
            case Direction.northwest:
                result = 315.0f;
                break;
        }
        return result;
    }
    public static Coordinate toCoord(this Direction dir)
    {
        Coordinate result;
        switch (dir)
        {
            case Direction.north:
                result = Coordinate.north;
                break;
            case Direction.west:
                result = Coordinate.west;
                break;
            case Direction.south:
                result = Coordinate.south;
                break;
            case Direction.east:
                result = Coordinate.east;
                break;
            default:
                result = Coordinate.zero;
                break;
            case Direction.northeast:
                result = Coordinate.northeast;
                break;
            case Direction.southeast:
                result = Coordinate.southeast;
                break;
            case Direction.southwest:
                result = Coordinate.southwest;
                break;
            case Direction.northwest:
                result = Coordinate.northwest;
                break;
        }
        return result;
    }

    public static float? toAngle(this Direction dir)
    {
        float? result = null;
        if(dir!=Direction.nothing)
            result = 90 * (float)dir;
        return result;
    }

    public static int GetQuadrant(this Direction dir)
    {
        int result;
        switch (dir) {
            case Direction.north:
                result = 0;
                break;
            case Direction.east:
                result = 1;
                break;
            case Direction.south:
                result = 2;
                break;
            case Direction.west:
                result = 3;
                break;
            default:
                result = 0;
                break;
        }
        return result;
    }

    public static Direction QuadrantRotate(this Direction dir, int quadrants)
    {
        for (int i = 0; i < quadrants; i++) {
            switch (dir) {
                case Direction.north:
                    dir = Direction.east;
                    break;
                case Direction.east:
                    dir = Direction.south;
                    break;
                case Direction.south:
                    dir = Direction.west;
                    break;
                case Direction.west:
                    dir = Direction.north;
                    break;
                case Direction.northeast:
                    dir = Direction.southeast;
                    break;
                case Direction.southeast:
                    dir = Direction.southwest;
                    break;
                case Direction.southwest:
                    dir = Direction.northwest;
                    break;
                case Direction.northwest:
                    dir = Direction.northeast;
                    break;
                default:
                    dir = Direction.nothing;
                    break;
            }
        }
        return dir;
    }

    public static Direction toDirection(this string type)
    {
        return (Direction)Enum.Parse(typeof(Direction), type);
    }

}
