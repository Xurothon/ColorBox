using System.Collections.Generic;
using UnityEngine;

public class TutorialBoardCreator : MonoBehaviour
{
    [SerializeField] private Sprite _sprite1;
    [SerializeField] private Sprite _sprite2;
    [SerializeField] private Sprite _sprite3;
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
            }
        }
        ChangeSprite (tileArray);
        for (int y = 0; y < _ySize; y++)
        {
            Tile newTile = Instantiate (_tile, transform.position, Quaternion.identity);
            newTile.transform.position = new Vector3 (xPos + (tileSize.x * _xSize), yPos + (tileSize.y * y), 0);
            newTile.transform.parent = transform;
            newTile.spriteRenderer.sprite = null;
        }
        return tileArray;
    }

    private void ChangeSprite (Tile[, ] tileArray)
    {
        tileArray[0, 2].spriteRenderer.sprite = _sprite2;
        tileArray[1, 2].spriteRenderer.sprite = _sprite3;
        tileArray[2, 2].spriteRenderer.sprite = _sprite1;
        Sprite[] sprites = { _sprite1, _sprite2, _sprite3 };
        for (int x = 0; x < _xSize; x++)
        {
            for (int y = _ySize - 2; y > -1; y--)
            {
                tileArray[x, y].spriteRenderer.sprite = sprites[x];
            }

        }
    }
}