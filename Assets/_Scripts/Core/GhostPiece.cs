using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GhostPiece : MonoBehaviour
{
    [SerializeField] private Tile _tile;
    [SerializeField] private Board _board;
    [SerializeField] private Piece _trackingPiece;
    [SerializeField] private Tilemap _tilemap;

    private Vector3Int[] _cells;
    public Vector3Int[] Cells => _cells;

    private Vector3Int _position;

    public Vector3Int Position => _position;


    private void Awake()
    {
        // _cells = new Vector3Int[_trackingPiece.Cells.Length]; TODO: Make it less rigid

        _cells = new Vector3Int[4];
    }


    private void LateUpdate()
    {
        Clear();
        Copy();
        Drop();
        Set();
    }

    public void Clear()
    {
        foreach (var cell in _cells)
        {
            var tilePosition = cell + _position;
            _tilemap.SetTile(tilePosition, null);
        }
    }

    public void Copy()
    {
        for (int i = 0; i < _cells.Length; i++)
        {
            _cells[i] = _trackingPiece.Cells[i];
        }
    }


    public void Drop()
    {
        Vector3Int position = _trackingPiece.Position;

        int current = position.y;
        int bottom = -_board.BoardSize.y / 2 - 1;

        _board.Clear(_trackingPiece);

        for (int row = current; row >= bottom; row--)
        {
            position.y = row;
            if (_board.IsValidPosition(_trackingPiece, position))
            {
                _position = position;
            }
            else
            {
                break;
            }
        }

        _board.Set(_trackingPiece);
    }

    public void Set()
    {
        foreach (var cell in _cells)
        {
            var tilePosition = cell + _position;
            _tilemap.SetTile(tilePosition, _tile);
        }
    }
}