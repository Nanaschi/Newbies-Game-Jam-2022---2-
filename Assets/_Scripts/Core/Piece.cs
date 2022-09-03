using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Core;
using UnityEngine;

public class Piece : MonoBehaviour //TODO: Make it non-monobehaviour
{
    private Board _board;
    private Vector3Int[] _cells;
    private int _currentIndex;
    public Vector3Int[] Cells => _cells;

    private Vector3Int _position;

    public Board Board => _board;

    public Vector3Int Position => _position;

    private TetrominoData _tetrominoData;

    public TetrominoData TetrominoData => _tetrominoData;

    public void Initialize(Board board, Vector3Int position, TetrominoData tetrominoData)
    {
        _tetrominoData = tetrominoData;
        _position = position;
        _board = board;
        _cells ??= new Vector3Int[tetrominoData.Cells.Length];

        for (int i = 0; i < tetrominoData.Cells.Length; i++)
        {
            _cells[i] = (Vector3Int) tetrominoData.Cells[i];
        }
    }

    private void Update()
    {
        _board.Clear(this);

        RotateLogic();
        MoveLogic();
        
        _board.Set(this);
    }

    private void RotateLogic()
    {
        if (Input.GetKeyDown(KeyCode.Q)) Rotate(-1);
        else if (Input.GetKeyDown(KeyCode.E)) Rotate(1);
    }

    private void Rotate(int direction)
    {
        _currentIndex += direction;
        _currentIndex.Wrap(0, 4);

        for (int i = 0; i < _cells.Length; i++)
        {
            Vector3 cell = _cells[i];

            switch (_tetrominoData.Tetromino)
            {
                case (Tetromino.I):
                case (Tetromino.O):
                    //.5f logic
                break;
                default:
                    //common logic
                break;
            }
        }
    }

    private void MoveLogic()
    {
        if (Input.GetKeyDown(KeyCode.A)) Move(Vector2Int.left);
        else if (Input.GetKeyDown(KeyCode.D)) Move(Vector2Int.right);
        if (Input.GetKeyDown(KeyCode.S)) Move(Vector2Int.down);
        if (Input.GetKeyDown(KeyCode.Space)) HardDrop();
    }

    private void HardDrop()
    {
        while (Move(Vector2Int.down)) continue;
    }

    private bool Move(Vector2Int translation)
    {
        var newPosition = _position;
        newPosition += (Vector3Int) translation;

        var valid = _board.IsValidPosition(this, newPosition);
        if (valid)
        {
            _position = newPosition;
        }

        return valid;
    }
}