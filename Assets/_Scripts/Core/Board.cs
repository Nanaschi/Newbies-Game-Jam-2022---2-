using UnityEngine;
using UnityEngine.Tilemaps;

public class Board : MonoBehaviour
{
    [SerializeField] private TetrominoData[] _tetrominoData;

    [SerializeField] private Tilemap _tilemap;
    [SerializeField] private Piece _activePiece;
    [SerializeField] private Vector3Int spawnPosition;

    private void Awake()
    {
        for (var i = 0; i < _tetrominoData.Length; i++)
        {
            _tetrominoData[i].Initialize();
        }
    }

    private void Start()
    {
        SpawnPiece();
    }

    private void SpawnPiece()
    {
        var spawnedNumber = Random.Range(0, _tetrominoData.Length);
        var spawnedTetromino = _tetrominoData[spawnedNumber];
        
        _activePiece.Initialize(this, spawnPosition, spawnedTetromino);
        
        Set(_activePiece);
    }

    private void Set(Piece piece)
    {
        foreach (var tilePosition in piece.Cells)
        {
            _tilemap.SetTile(tilePosition, piece.TetrominoData.Tile);
        }
    }
}