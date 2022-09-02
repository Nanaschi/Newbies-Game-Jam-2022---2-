using System.Collections.Generic;
using UnityEngine;

public static class Data
{
    public static readonly float cos = Mathf.Cos(Mathf.PI / 2f);
    public static readonly float sin = Mathf.Sin(Mathf.PI / 2f);
    public static readonly float[] RotationMatrix = new float[] {cos, sin, -sin, cos};

    public static readonly Dictionary<Tetromino, Vector2Int[]> Cells =
        new Dictionary<Tetromino, Vector2Int[]>()
        {
            {
                Tetromino.Straight,
                new Vector2Int[]
                {
                    new Vector2Int(-1, 1), new Vector2Int(0, 1), new Vector2Int(1, 1),
                    new Vector2Int(2, 1)
                }
            },
            {
                Tetromino.LShape,
                new Vector2Int[]
                {
                    new Vector2Int(1, 1), new Vector2Int(-1, 0), new Vector2Int(0, 0),
                    new Vector2Int(1, 0)
                }
            },
            {
                Tetromino.Square,
                new Vector2Int[]
                {
                    new Vector2Int(0, 1), new Vector2Int(1, 1), new Vector2Int(0, 0),
                    new Vector2Int(1, 0)
                }
            },
            {
                Tetromino.Skew,
                new Vector2Int[]
                {
                    new Vector2Int(0, 1), new Vector2Int(1, 1), new Vector2Int(-1, 0),
                    new Vector2Int(0, 0)
                }
            },
            {
                Tetromino.TShape,
                new Vector2Int[]
                {
                    new Vector2Int(0, 1), new Vector2Int(-1, 0), new Vector2Int(0, 0),
                    new Vector2Int(1, 0)
                }
            },
        };

    private static readonly Vector2Int[,] WallKicksI = new Vector2Int[,]
    {
        {
            new Vector2Int(0, 0), new Vector2Int(-2, 0), new Vector2Int(1, 0),
            new Vector2Int(-2, -1), new Vector2Int(1, 2)
        },
        {
            new Vector2Int(0, 0), new Vector2Int(2, 0), new Vector2Int(-1, 0), new Vector2Int(2, 1),
            new Vector2Int(-1, -2)
        },
        {
            new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int(2, 0),
            new Vector2Int(-1, 2), new Vector2Int(2, -1)
        },
        {
            new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(-2, 0),
            new Vector2Int(1, -2), new Vector2Int(-2, 1)
        },
        {
            new Vector2Int(0, 0), new Vector2Int(2, 0), new Vector2Int(-1, 0), new Vector2Int(2, 1),
            new Vector2Int(-1, -2)
        },
        {
            new Vector2Int(0, 0), new Vector2Int(-2, 0), new Vector2Int(1, 0),
            new Vector2Int(-2, -1), new Vector2Int(1, 2)
        },
        {
            new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(-2, 0),
            new Vector2Int(1, -2), new Vector2Int(-2, 1)
        },
        {
            new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int(2, 0),
            new Vector2Int(-1, 2), new Vector2Int(2, -1)
        },
    };

    private static readonly Vector2Int[,] WallKicksJLOSTZ = new Vector2Int[,]
    {
        {
            new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int(-1, 1),
            new Vector2Int(0, -2), new Vector2Int(-1, -2)
        },
        {
            new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(1, -1), new Vector2Int(0, 2),
            new Vector2Int(1, 2)
        },
        {
            new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(1, -1), new Vector2Int(0, 2),
            new Vector2Int(1, 2)
        },
        {
            new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int(-1, 1),
            new Vector2Int(0, -2), new Vector2Int(-1, -2)
        },
        {
            new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(1, 1), new Vector2Int(0, -2),
            new Vector2Int(1, -2)
        },
        {
            new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int(-1, -1),
            new Vector2Int(0, 2), new Vector2Int(-1, 2)
        },
        {
            new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int(-1, -1),
            new Vector2Int(0, 2), new Vector2Int(-1, 2)
        },
        {
            new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(1, 1), new Vector2Int(0, -2),
            new Vector2Int(1, -2)
        },
    };

    public static readonly Dictionary<Tetromino, Vector2Int[,]> WallKicks =
        new Dictionary<Tetromino, Vector2Int[,]>()
        {
            {Tetromino.Straight, WallKicksI},
            {Tetromino.LShape, WallKicksJLOSTZ},
            {Tetromino.Square, WallKicksJLOSTZ},
            {Tetromino.Skew, WallKicksJLOSTZ},
            {Tetromino.TShape, WallKicksJLOSTZ}
        };
}