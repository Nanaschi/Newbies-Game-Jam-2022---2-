using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum Tetromino 
{
    Straight,
    Square,
    TShape,
    LShape,
    Skew
}

[Serializable]
public struct TetrominoData //TODO: Make it Scriptable object 
{
    private Tetromino _tetromino;
    private Tile _tile;
    private Vector2Int[] _cells;

    public Tetromino Tetromino => _tetromino;

    public Tile Tile => _tile;

    public Vector2Int[] Cells => _cells;

    public void Initialize()
    {
        _cells = Data.Cells[_tetromino];
    }
}
