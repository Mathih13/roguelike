using InfallibleCode;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Direction
{
    public readonly Vector3 Vector;

    public bool IsFacingLeft => Vector.x < 0;

    private Direction(float x, float y, float z)
    {
        Vector = new Vector3(x, y, z);
    }

    public static Direction NearestFromVector(Vector3 vector)
    {
        vector.Round();
        vector = vector.normalized;
        vector = vector.SnapTo(90);
        if (vector.x == vector.y)
        {
            if (UnityEngine.Random.Range(0, 1) == 1)
            {
                vector = new Vector3(vector.x, 0, 0);
            }
            else
            {
                vector = new Vector3(0, vector.y, 0);

            }

        }

        //v

        var result = Directions
            .FirstOrDefault(direction => direction == vector);

        if (result != null)
        {
            return result;
        }
        return None;
    }

    public static Direction Random => Directions.ElementAt(UnityEngine.Random.Range(0, Directions.Count));

    public static implicit operator Vector3(Direction direction)
    {
        return direction.Vector;
    }

    public static readonly Direction None = new Direction(0, 0, 0);
    public static readonly Direction North = new Direction(0, 1, 0);
    public static readonly Direction South = new Direction(0, -1, 0);
    public static readonly Direction East = new Direction(-1, 0, 0);
    public static readonly Direction West = new Direction(1, 0, 0);
    //public static readonly Direction Northeast = new Direction(1, 1, 0);
    //public static readonly Direction Northwest = new Direction(-1, 1, 0);
    //public static readonly Direction Southeast = new Direction(1, -1, 0);
    //public static readonly Direction Southwest = new Direction(-1, -1, 0);

    public static readonly List<Direction> Directions = new List<Direction>
        {
            None,
            North,

            South,

            East,
            West
        };
}