using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Core;
using UnityEngine;

public class Piece : MonoBehaviour //TODO: Make it non-monobehaviour
{
    private Board _board;
    private Vector3Int[] _cells;
    private int _rotationIndex;
    public Vector3Int[] Cells => _cells;

    private Vector3Int _position;

    [SerializeField] private float _stepDelay;
    [SerializeField] private float _lockDelay;

    private float _stepTime;
    private float _lockTime;


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
        _stepTime = Time.time + _stepDelay;
        _lockTime = 0;

        for (int i = 0; i < tetrominoData.Cells.Length; i++)
        {
            _cells[i] = (Vector3Int) tetrominoData.Cells[i];
        }
    }

    private void Update()
    {
        _board.Clear(this);

        _lockTime += Time.deltaTime;

        RotateLogic();
        MoveLogic();
        StepLogic();


        _board.Set(this);
    }

    private void StepLogic()
    {
        if (Time.time <= _stepTime) return;

        _stepTime = Time.time + _stepDelay;

        Move(Vector2Int.down);
        
        if (_lockTime <= _lockDelay) return;

        LockPiece();
    }

    private void LockPiece()
    {
        _board.Set(this);
        _board.ClearLines();
        _board.SpawnPiece();
    }

    private void RotateLogic()
    {
        if (Input.GetKeyDown(KeyCode.Q)) Rotate(-1);
        else if (Input.GetKeyDown(KeyCode.E)) Rotate(1);
    }

    private void Rotate(int direction)
    {
        int originalRotation = _rotationIndex;

        float[] matrix = Data.RotationMatrix;

        _rotationIndex = Wrap(_rotationIndex + direction, 0, 4);

        ApplyRotationMatrix(direction, matrix);

        if (!TestWallKicks(_rotationIndex, direction))
        {
            _rotationIndex = originalRotation;
            ApplyRotationMatrix(-direction, matrix);
        }
    }

    private void ApplyRotationMatrix(int direction, float[] matrix)
    {
        for (int i = 0; i < _cells.Length; i++)
        {
            Vector3 cell = _cells[i];

            int x, y;

            switch (_tetrominoData.Tetromino)
            {
                case (Tetromino.I):
                case (Tetromino.O):
                    //.5f logic
                    cell.x -= 0.5f;
                    cell.y -= 0.5f;
                    x = Mathf.CeilToInt((cell.x * matrix[0] * direction) +
                                        (cell.y * matrix[1] * direction));
                    y = Mathf.CeilToInt((cell.x * matrix[2] * direction) +
                                        (cell.y * matrix[3] * direction));
                    break;
                default:
                    //common logic
                    x = Mathf.RoundToInt((cell.x * matrix[0] * direction) +
                                         (cell.y * matrix[1] * direction));
                    y = Mathf.RoundToInt((cell.x * matrix[2] * direction) +
                                         (cell.y * matrix[3] * direction));

                    break;
            }

            _cells[i] = new Vector3Int(x, y, 0);
        }
    }


    public bool TestWallKicks(int rotationIndex, int rotationDirection)
    {
        var getWallKickIndex = GetWallKickIndex(rotationIndex, rotationDirection);
        for (int i = 0; i < _tetrominoData.WallKicks.GetLength(1); i++)
        {
            var tetrominoDataWallKick = _tetrominoData.WallKicks[getWallKickIndex, i];
            if (Move(tetrominoDataWallKick)) return true;
        }

        return false;
    }

    public int GetWallKickIndex(int rotationIndex, int rotationDirection)
    {
        int wallKickIndex = rotationIndex * 2;
        if (rotationDirection < 0) wallKickIndex--;
        return Wrap(wallKickIndex, 0, _tetrominoData.WallKicks.GetLength(0));
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
        LockPiece();
    }

    private bool Move(Vector2Int translation)
    {
        var newPosition = _position;
        newPosition += (Vector3Int) translation;

        var valid = _board.IsValidPosition(this, newPosition);
        if (valid)
        {
            _position = newPosition;
            _lockTime = 0;
        }

        return valid;
    }

    public int Wrap(int input, int min, int max)
    {
        if (input < min)
        {
            return max - (min - input) % (max - min);
        }
        else
        {
            return min + (input - min) % (max - min);
        }
    }
}