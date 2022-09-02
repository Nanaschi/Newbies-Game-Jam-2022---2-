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
    [SerializeField] private Tetromino _tetromino;
    [SerializeField] private Tile _tile;
    public Tetromino Tetromino => _tetromino;

    public Tile Tile => _tile;

    
    private Vector2Int[] _cells;
    public Vector2Int[] Cells => _cells;

    public void Initialize()
    {
        _cells = Data.Cells[_tetromino];
    }
}
