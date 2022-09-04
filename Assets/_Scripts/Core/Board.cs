using UnityEngine;
using UnityEngine.Tilemaps;

public class Board : MonoBehaviour
{
    [SerializeField] private TetrominoData[] _tetrominoData;

    [SerializeField] private Tilemap _tilemap;
    [SerializeField] private Piece _activePiece;
    [SerializeField] private Vector3Int _spawnPosition;
    private Vector2Int _boardSize = new Vector2Int(10, 20);

    public Vector2Int BoardSize => _boardSize;

    RectInt Bounds
    {
        get
        {
            var position = new Vector2Int(-_boardSize.x, -_boardSize.y) / 2;
            return new RectInt(position, _boardSize);
        }
    }

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

    public void SpawnPiece()
    {
        var spawnedNumber = Random.Range(0, _tetrominoData.Length);
        var spawnedTetromino = _tetrominoData[spawnedNumber];

        _activePiece.Initialize(this, _spawnPosition, spawnedTetromino); //TODO: interdependence 


        if (IsValidPosition(_activePiece, _spawnPosition))
        {
            Set(_activePiece);
        }
        else
        {
            GameOver();
        }
        

    }

    private void GameOver()
    {
        _tilemap.ClearAllTiles();
    }

    public void Set(Piece piece)
    {
        foreach (var cell in piece.Cells)
        {
            var tilePosition = cell + piece.Position;
            _tilemap.SetTile(tilePosition, piece.TetrominoData.Tile);
        }
    }

    public void Clear(Piece piece) //TODO: Make it automatic after set if it is available
    {
        foreach (var cell in piece.Cells)
        {
            var tilePosition = cell + piece.Position;
            _tilemap.SetTile(tilePosition, null);
        }
    }

    public bool IsValidPosition(Piece piece, Vector3Int vector3Int)
    {
        var bounds = Bounds;

        foreach (var cell in piece.Cells)
        {
            var tilePosition = cell + vector3Int;
            if (_tilemap.HasTile(tilePosition) || !bounds.Contains((Vector2Int) tilePosition))
                return false;
        }

        return true;
    }

    public void ClearLines()
    {
        var boardBounds = Bounds;
        int rowCheck = boardBounds.yMin;


        while (rowCheck < boardBounds.yMax)
        {
            if (IsLineFull(rowCheck)) LineClear(rowCheck);
            else rowCheck++;
        }
    }

    private void LineClear(int row)
    {
        var boardBounds = Bounds;
        for (int i = boardBounds.xMin; i < boardBounds.xMax; i++)
        {
            var position = new Vector3Int(i, row, 0);
            _tilemap.SetTile(position, null);
        }

        while (row < boardBounds.yMax)
        {
            for (int i = boardBounds.xMin; i < boardBounds.xMax; i++)
            {
                var position = new Vector3Int(i, row + 1, 0);
                var tileBaseAbove = _tilemap.GetTile(position);
                
                position = new Vector3Int(i, row , 0);
                _tilemap.SetTile(position, tileBaseAbove);
            }

            row++;
        }
    }


    private bool IsLineFull(int row)
    {
        var boardBounds = Bounds;
        for (int i = boardBounds.xMin; i < boardBounds.xMax; i++)
        {
            var position = new Vector3Int(i, row, 0);
            if(!_tilemap.HasTile(position)) return false;
        }
        return true;
    }
}