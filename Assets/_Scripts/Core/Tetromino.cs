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
public struct TetrominoData
{
    public Tetromino _tetromino;
    public Tile _tile;
}
