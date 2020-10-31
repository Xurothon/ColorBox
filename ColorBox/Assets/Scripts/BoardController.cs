using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour
{
    private int _xSize, _ySize;
    private List<Sprite> _tileSprites = new List<Sprite> ();
    private Tile[, ] _tiles;
    private Vector2[] dirRay = new Vector2[] { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
    private bool _isFindMatch;
    private bool _isSearchEmptyTile;

    public void SetValues (BoardSettings boardSettings, Tile[, ] tiles)
    {
        _xSize = boardSettings.xSize;
        _ySize = boardSettings.ySize;
        _tileSprites = boardSettings.tileSprites;
        _tiles = tiles;
    }

    public void SwapTwoTiles (MainTile mainTile, Tile tile)
    {
        if (!tile.isEmpty)
        {
            Sprite cashSprite = mainTile.image.sprite;
            mainTile.image.sprite = tile.spriteRenderer.sprite;
            tile.spriteRenderer.sprite = cashSprite;
            FindAllMatch (tile);
        }
    }

    private void Update ()
    {
        if (_isSearchEmptyTile)
        {
            SearchEmptyTile ();
        }
    }

    private List<Tile> FindMatch (Tile tile, Vector2 dir)
    {
        List<Tile> cashFindTile = new List<Tile> ();
        RaycastHit2D hit = Physics2D.Raycast (tile.transform.position, dir);
        while (hit.collider != null && hit.collider.gameObject.GetComponent<Tile> ().spriteRenderer.sprite == tile.spriteRenderer.sprite)
        {
            cashFindTile.Add (hit.collider.gameObject.GetComponent<Tile> ());
            hit = Physics2D.Raycast (hit.collider.transform.position, dir);
        }
        return cashFindTile;
    }

    private void DeleteSprite (Tile tile, Vector2[] dirArray)
    {
        List<Tile> cashFindSprite = new List<Tile> ();
        for (int i = 0; i < dirArray.Length; i++)
        {
            cashFindSprite.AddRange (FindMatch (tile, dirArray[i]));
        }
        if (cashFindSprite.Count >= 2)
        {
            for (int i = 0; i < cashFindSprite.Count; i++)
            {
                cashFindSprite[i].spriteRenderer.sprite = null;
            }
            _isFindMatch = true;
        }
    }

    private void FindAllMatch (Tile tile)
    {
        if (tile.isEmpty) return;
        DeleteSprite (tile, new Vector2[] { Vector2.up, Vector2.down });
        DeleteSprite (tile, new Vector2[] { Vector2.left, Vector2.right });
        if (_isFindMatch)
        {
            _isFindMatch = false;
            tile.spriteRenderer.sprite = null;
            _isSearchEmptyTile = true;
        }
    }

    private void SearchEmptyTile ()
    {
        for (int x = 0; x < _xSize; x++)
        {
            for (int y = 0; y < _ySize; y++)
            {
                if (x == 0 && y == 0)
                {
                    _isSearchEmptyTile = false;
                }
                if (_tiles[x, y].isEmpty)
                {
                    ShiftTileDown (x, y);
                    break;
                }
            }
        }
    }

    private void ShiftTileDown (int xPos, int yPos)
    {
        List<SpriteRenderer> cashRenderer = new List<SpriteRenderer> ();
        for (int y = yPos; y < _ySize; y++)
        {
            Tile tile = _tiles[xPos, y];
            if (!tile.isEmpty)
            {
                cashRenderer.Add (tile.spriteRenderer);
            }
        }
        SetNewSprite (xPos, yPos, cashRenderer);
    }

    private void SetNewSprite (int xPos, int yPos, List<SpriteRenderer> renderers)
    {
        int yEndPos = yPos + renderers.Count;
        for (int y = yPos; y < yEndPos; y++)
        {
            _tiles[xPos, y].spriteRenderer.sprite = renderers[y - yPos].sprite;
        }
        for (int y = yEndPos; y < _ySize; y++)
        {
            _tiles[xPos, y].spriteRenderer.sprite = null;
        }

    }

}