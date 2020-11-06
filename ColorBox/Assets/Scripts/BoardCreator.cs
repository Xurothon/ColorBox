using System.Collections.Generic;
using UnityEngine;

public class BoardCreator : MonoBehaviour
{

    [SerializeField] private int _maxBlocksCount;
    [SerializeField] private int _minBlockCount;
    private Sprite _blockSprite;
    private bool _useBlocks;
    private int _xSize, _ySize;
    private Tile _tile;
    private List<Sprite> _tileSprites = new List<Sprite> ();
    private Sprite _cashSprite = null;

    public Tile[, ] SetValues (BoardSettings boardSettings)
    {
        _xSize = boardSettings.xSize;
        _ySize = boardSettings.ySize;
        _tile = boardSettings.tile;
        _tileSprites = boardSettings.tileSprites;
        _useBlocks = boardSettings.useBlocks;
        _blockSprite = boardSettings.blockSprite;
        return CreateBoard ();
    }

    private Tile[, ] CreateBoard ()
    {
        Tile[, ] tileArray = new Tile[_xSize, _ySize];
        float xPos = transform.position.x;
        float yPos = transform.position.y;
        Vector2 tileSize = _tile.spriteRenderer.bounds.size;
        for (int x = 0; x < _xSize; x++)
        {
            for (int y = 0; y < _ySize; y++)
            {
                Tile newTile = Instantiate (_tile, transform.position, Quaternion.identity);
                newTile.transform.position = new Vector3 (xPos + (tileSize.x * x), yPos + (tileSize.y * y), 0);
                newTile.transform.parent = transform;
                tileArray[x, y] = newTile;
                List<Sprite> tempSprite = new List<Sprite> ();
                tempSprite.AddRange (_tileSprites);
                tempSprite.Remove (_cashSprite);
                if (x > 0)
                {
                    tempSprite.Remove (tileArray[x - 1, y].spriteRenderer.sprite);
                }
                _cashSprite = tempSprite[Random.Range (0, tempSprite.Count)];
                newTile.spriteRenderer.sprite = _cashSprite;
            }
        }
        for (int y = 0; y < _ySize; y++)
        {
            Tile newTile = Instantiate (_tile, transform.position, Quaternion.identity);
            newTile.transform.position = new Vector3 (xPos + (tileSize.x * _xSize), yPos + (tileSize.y * y), 0);
            newTile.transform.parent = transform;
            newTile.spriteRenderer.sprite = null;
        }
        if (_useBlocks)
        {
            CreateBlocks (tileArray);
        }
        return tileArray;
    }

    private void CreateBlocks (Tile[, ] tiles)
    {
        int blockCount = Random.Range (_minBlockCount, _maxBlocksCount + 1);
        List<int> xBlockPositions = new List<int> ();
        while (xBlockPositions.Count < blockCount)
        {
            int tempXPosition = Random.Range (0, _xSize);
            if (!xBlockPositions.Contains (tempXPosition))
            {
                xBlockPositions.Add (tempXPosition);
            }
        }

        for (int i = 0; i < xBlockPositions.Count; i++)
        {
            int tempYPosition = Random.Range (1, _ySize - 1);
            CreateOneBlock (tiles[xBlockPositions[i], tempYPosition], xBlockPositions[i], tempYPosition);
            int nextYPositionBlock = GetNextYPosition ();
            CreateOneBlock (tiles[xBlockPositions[i], tempYPosition + nextYPositionBlock], xBlockPositions[i], tempYPosition + nextYPositionBlock);
        }
    }

    private void CreateOneBlock (Tile tile, int xPos, int yPos)
    {
        tile.isBlock = true;
        tile.spriteRenderer.sprite = _blockSprite;
        tile.xPosition = xPos;
        tile.yPosition = yPos;
    }

    private int GetNextYPosition ()
    {
        int tempValue = Random.Range (0, 2);
        if (tempValue == 0) return -1;
        return tempValue;
    }
}