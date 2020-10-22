using System.Collections.Generic;
using UnityEngine;

public class BoardCreator : MonoBehaviour
{
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
        return tileArray;
    }
}