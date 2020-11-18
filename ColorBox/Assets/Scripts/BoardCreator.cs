using System.Collections.Generic;
using UnityEngine;

public class BoardCreator : MonoBehaviour
{
    [SerializeField] private Sprite _playerSprite;
    [SerializeField] private Sprite _enemySprite;
    [SerializeField] private Sprite _blockSprite;
    [SerializeField] private Sprite[] _tileSprites;
    private bool _useBlocks;
    private int _xSize, _ySize;
    private Tile _tile;
    private Sprite _cashSprite = null;
    private LevelTileArrays _levelTileArray;

    public Tile[, ] SetValues (BoardSettings boardSettings)
    {
        _xSize = boardSettings.xSize;
        _ySize = boardSettings.ySize;
        _tile = boardSettings.tile;
        _useBlocks = boardSettings.useBlocks;
        _levelTileArray = new LevelTileArrays ();
        return CreateBoard ();
    }

    private Tile[, ] CreateBoard ()
    {
        Tile[, ] tileArray = new Tile[_xSize, _ySize];
        float xPos = transform.position.x;
        float yPos = transform.position.y;
        Vector2 tileSize = _tile.spriteRenderer.bounds.size;
        int currentScene = int.Parse (UnityEngine.SceneManagement.SceneManager.GetActiveScene ().name);
        TileType[, ] tileTypes = _levelTileArray.GetLevelArray (currentScene);
        for (int y = 0; y < _ySize; y++)
        {
            for (int x = 0; x < _xSize; x++)
            {
                Tile newTile = Instantiate (_tile, transform.position, Quaternion.identity);
                newTile.transform.position = new Vector3 (xPos + (tileSize.x * x), yPos + (tileSize.y * y), 0);
                newTile.transform.parent = transform;
                tileArray[x, y] = newTile;
                newTile.spriteRenderer.sprite = GetTileSprite (tileTypes[y, x]);
                if (tileTypes[y, x] == TileType.BLOCK)
                {
                    newTile.xPosition = x;
                    newTile.yPosition = y;
                    newTile.isBlock = true;
                }
            }
        }
        for (int y = 0; y < _ySize; y++)
        {
            Tile newTile = Instantiate (_tile, transform.position, Quaternion.identity);
            newTile.transform.position = new Vector3 (xPos + (tileSize.x * _xSize), yPos + (tileSize.y * y), 0);
            newTile.transform.parent = transform;
            newTile.spriteRenderer.sprite = null;
        }
        return tileArray;
    }

    private Sprite GetTileSprite (TileType tileType)
    {
        switch (tileType)
        {
            case TileType.TYPE1:
                return _tileSprites[0];
            case TileType.TYPE2:
                return _tileSprites[1];
            case TileType.TYPE3:
                return _tileSprites[2];
            case TileType.BLOCK:
                return _blockSprite;
            case TileType.PLAYER:
                return _playerSprite;
            case TileType.ENEMY:
                return _enemySprite;
        }
        return _tileSprites[0];
    }
}