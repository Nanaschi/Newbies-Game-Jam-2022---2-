using UnityEngine;
using UnityEngine.Tilemaps;

public class Board : MonoBehaviour
{
    [SerializeField] private TetrominoData[] _tetrominoData;

    [SerializeField] private Tilemap _tilemap;

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
    }
}
