using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour //TODO: Make it non-monobehaviour
{
    private Board _board;
    private Vector3Int[] _cells;

    public Vector3Int[] Cells => _cells;

    private Vector3Int _piecePosition;
    private TetrominoData _tetrominoData;

    public TetrominoData TetrominoData => _tetrominoData;

    public void Initialize(Board board,Vector3Int vector3Int, TetrominoData tetrominoData)
    {
        _tetrominoData = tetrominoData;
        _piecePosition = vector3Int;
        _board = board;
        _cells ??= new Vector3Int[tetrominoData.Cells.Length];

        for (int i = 0; i < tetrominoData.Cells.Length; i++)
        {
            _cells[i] = (Vector3Int) tetrominoData.Cells[i];
        }
    }
}
