using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour
{
    private int _xSize, _ySize;
    private List<Sprite> _tileSprites = new List<Sprite> ();
    private Tile[, ] _tiles;
    private Vector2[] dirRay = new Vector2[] { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
    private bool _isFindMatch;
    private bool _isShift;
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
        Sprite cashSprite = mainTile.image.sprite;
        mainTile.image.sprite = tile.spriteRenderer.sprite;
        tile.spriteRenderer.sprite = cashSprite;
        FindAllMatch (tile);
    }

    private void Update ()
    {
        if (_isSearchEmptyTile)
        {
            Debug.Log ("Update");
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
            for (int y = _ySize - 1; y > -1; y--)
            {
                if (_tiles[x, y].isEmpty)
                {
                    ShiftTileDown (x, y);
                    //break;
                }
                if (x == _xSize - 1 && y == 0)
                {
                    _isSearchEmptyTile = false;
                }

            }
        }
    }

    private void ShiftTileDown (int xPos, int yPos)
    {
        _isShift = true;
        List<SpriteRenderer> cashRenderer = new List<SpriteRenderer> ();
        for (int y = yPos; y < _ySize - 1; y++)
        {
            Tile tile = _tiles[xPos, y + 1];
            if (tile.isEmpty)
            {
                //cashRenderer.Add (tile.spriteRenderer);
                _tiles[xPos, y].spriteRenderer.sprite = _tiles[xPos, y + 1].spriteRenderer.sprite;
            }
        }
        SetNewSprite (xPos, cashRenderer);
        _isShift = false;
    }

    private void SetNewSprite (int xPos, List<SpriteRenderer> renderers)
    {
        for (int y = 0; y < renderers.Count - 1; y++)
        {
            renderers[y].sprite = renderers[y + 1].sprite;
            renderers[y + 1].sprite = _tileSprites[0];
        }
    }

}