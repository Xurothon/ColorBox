using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour
{
    [SerializeField] private GravityChanger _gravityChganger;
    [SerializeField] private StepCancel _stepCancel;
    private int _xSize, _ySize;
    private List<Sprite> _tileSprites = new List<Sprite> ();
    private Tile[, ] _tiles;
    private Vector2[] dirRay = new Vector2[] { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
    private bool _isFindMatch;
    private bool _isLevelComplete;
    public void SetValues (BoardSettings boardSettings, Tile[, ] tiles)
    {
        _xSize = boardSettings.xSize;
        _ySize = boardSettings.ySize;
        _tileSprites = boardSettings.tileSprites;
        _tiles = tiles;
        _stepCancel.SetValues (boardSettings, tiles);
    }

    public void SwapTwoTiles (MainTile mainTile, Tile tile)
    {
        if (!tile.isEmpty)
        {
            _stepCancel.SavePreviosStep ();
            Sprite cashSprite = mainTile.image.sprite;
            mainTile.image.sprite = tile.spriteRenderer.sprite;
            tile.spriteRenderer.sprite = cashSprite;
            FindAllMatch (tile);
            SearchEmptyTile ();
            CheckLevelComplete ();
        }
    }

    public void ChageSpriteTile (Tile tile, Sprite sprite)
    {
        _stepCancel.SavePreviosStep ();
        tile.spriteRenderer.sprite = sprite;
        FindAllMatch (tile);
        SearchEmptyTile ();
        CheckLevelComplete ();
    }

    private void CheckLevelComplete ()
    {
        _isLevelComplete = true;
        for (int x = 0; x < _xSize; x++)
        {
            for (int y = 0; y < _ySize; y++)
            {
                if (_tiles[x, y].spriteRenderer.sprite != null)
                {
                    _isLevelComplete = false;
                }
            }
        }
        if (_isLevelComplete) GameUIHelper.Instance.ShowLevelCompletePanel ();
    }

    private void Start ()
    {
        _gravityChganger.OnGravityChange.AddListener (_stepCancel.SavePreviosStep);
        _gravityChganger.OnGravityChange.AddListener (SearchEmptyTile);
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
            if (cashFindSprite.Count >= 4)
            {
                DataWorker.Instance.AddCrystal (1);
            }
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
        }
    }

    private void SearchEmptyTile ()
    {
        for (int x = 0; x < _xSize; x++)
        {
            for (int y = 0; y < _ySize; y++)
            {
                if (_tiles[x, y].isEmpty)
                {
                    ChooseShiftDirection (x, y);
                }
            }
        }
    }

    private void ChooseShiftDirection (int xPos, int yPos)
    {
        switch (_gravityChganger.GetDirection ())
        {
            case GravityDirection.UP:
                {
                    ShiftTileUp (xPos, yPos);
                    break;
                }
            case GravityDirection.RIGHT:
                {
                    ShiftTileRight (xPos, yPos);
                    break;
                }
            case GravityDirection.LEFT:
                {
                    ShiftTileLeft (xPos, yPos);
                    break;
                }
            default:
                {
                    ShiftTileDown (xPos, yPos);
                    break;
                }
        }
    }

    private void ShiftTileDown (int xPos, int yPos)
    {
        List<SpriteRenderer> cashRenderer = new List<SpriteRenderer> ();
        for (int y = 0; y < _ySize; y++)
        {
            Tile tile = _tiles[xPos, y];
            if (!tile.isEmpty)
            {
                cashRenderer.Add (tile.spriteRenderer);
            }
        }
        SetNewSpriteDown (xPos, yPos, cashRenderer);
    }

    private void ShiftTileUp (int xPos, int yPos)
    {
        List<SpriteRenderer> cashRenderer = new List<SpriteRenderer> ();
        for (int y = _ySize - 1; y > -1; y--)
        {
            Tile tile = _tiles[xPos, y];
            if (!tile.isEmpty)
            {
                cashRenderer.Add (tile.spriteRenderer);
            }
        }
        SetNewSpriteUp (xPos, yPos, cashRenderer);
    }

    private void ShiftTileRight (int xPos, int yPos)
    {
        List<SpriteRenderer> cashRenderer = new List<SpriteRenderer> ();
        for (int x = _xSize - 1; x > -1; x--)
        {
            Tile tile = _tiles[x, yPos];
            if (!tile.isEmpty)
            {
                cashRenderer.Add (tile.spriteRenderer);
            }
        }
        SetNewSpriteRight (xPos, yPos, cashRenderer);
    }

    private void ShiftTileLeft (int xPos, int yPos)
    {
        List<SpriteRenderer> cashRenderer = new List<SpriteRenderer> ();
        for (int x = 0; x < _xSize; x++)
        {
            Tile tile = _tiles[x, yPos];
            if (!tile.isEmpty)
            {
                cashRenderer.Add (tile.spriteRenderer);
            }
        }
        SetNewSpriteLeft (xPos, yPos, cashRenderer);
    }

    private void SetNewSpriteDown (int xPos, int yPos, List<SpriteRenderer> renderers)
    {
        int yEndPos = renderers.Count;
        for (int y = 0; y < yEndPos; y++)
        {
            _tiles[xPos, y].spriteRenderer.sprite = renderers[y].sprite;
        }
        for (int y = yEndPos; y < _ySize; y++)
        {
            _tiles[xPos, y].spriteRenderer.sprite = null;
        }
    }

    private void SetNewSpriteUp (int xPos, int yPos, List<SpriteRenderer> renderers)
    {
        int yEndPos = _ySize - renderers.Count - 1;
        for (int y = _ySize - 1; y > yEndPos; y--)
        {
            _tiles[xPos, y].spriteRenderer.sprite = renderers[_ySize - 1 - y].sprite;
        }
        for (int y = yEndPos; y > -1; y--)
        {
            _tiles[xPos, y].spriteRenderer.sprite = null;
        }
    }

    private void SetNewSpriteRight (int xPos, int yPos, List<SpriteRenderer> renderers)
    {
        int xEndPos = _xSize - renderers.Count - 1;
        for (int x = _xSize - 1; x > xEndPos; x--)
        {
            _tiles[x, yPos].spriteRenderer.sprite = renderers[_xSize - 1 - x].sprite;
        }
        for (int x = xEndPos; x > -1; x--)
        {
            _tiles[x, yPos].spriteRenderer.sprite = null;
        }
    }

    private void SetNewSpriteLeft (int xPos, int yPos, List<SpriteRenderer> renderers)
    {
        int xEndPos = renderers.Count;
        for (int x = 0; x < xEndPos; x++)
        {
            _tiles[x, yPos].spriteRenderer.sprite = renderers[x].sprite;
        }
        for (int x = xEndPos; x < _xSize; x++)
        {
            _tiles[x, yPos].spriteRenderer.sprite = null;
        }
    }

}