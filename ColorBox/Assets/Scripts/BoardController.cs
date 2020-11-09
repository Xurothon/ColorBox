using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour
{
    [SerializeField] private GravityChanger _gravityChganger;
    [SerializeField] private StepCancel _stepCancel;
    private int _xSize, _ySize;
    private List<Sprite> _tileSprites = new List<Sprite> ();
    private Tile[, ] _tiles;
    private bool _isLevelComplete;
    private bool _useBlocks;
    private Sprite _blockSprite;

    public void SetValues (BoardSettings boardSettings, Tile[, ] tiles)
    {
        _xSize = boardSettings.xSize;
        _ySize = boardSettings.ySize;
        _tileSprites = boardSettings.tileSprites;
        _tiles = tiles;
        _useBlocks = boardSettings.useBlocks;
        _blockSprite = boardSettings.blockSprite;
        _stepCancel.SetValues (boardSettings, tiles);
    }

    public void SwapTwoTiles (MainTile mainTile, Tile tile)
    {
        if (!tile.isEmpty)
        {
            if (!tile.isBlock)
            {
                _stepCancel.SavePreviosStep ();
                Sprite cashSprite = mainTile.image.sprite;
                mainTile.image.sprite = tile.spriteRenderer.sprite;
                tile.spriteRenderer.sprite = cashSprite;
                FindAllMatch (tile);
            }
        }
    }

    public void ChangeSpriteTile (Tile tile, Sprite sprite)
    {
        _stepCancel.SavePreviosStep ();
        tile.spriteRenderer.sprite = sprite;
        FindAllMatch (tile);
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
        if (_isLevelComplete)
        {
            DataWorker.Instance.AddLevelCompleteCount ();
            GameUIHelper.Instance.ShowLevelCompletePanel ();
        }
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
        if (cashFindSprite.Count > 1)
        {
            if (cashFindSprite.Count > 2)
            {
                DataWorker.Instance.AddCrystal (1);
            }
            StartCoroutine (DeleteSprites (tile, cashFindSprite));
        }
    }

    private IEnumerator DeleteSprites (Tile tile, List<Tile> cashTiles)
    {
        tile.spriteRenderer.sprite = null;
        yield return new WaitForSeconds (0.15f);
        for (int i = 0; i < cashTiles.Count; i++)
        {
            SoundsHelper.Instance.PlayDisappearanceTile ();
            cashTiles[i].spriteRenderer.sprite = null;
            yield return new WaitForSeconds (0.15f);
        }
        SearchEmptyTile ();
        CheckLevelComplete ();
    }

    private void FindAllMatch (Tile tile)
    {
        if (tile.isEmpty) return;
        DeleteSprite (tile, new Vector2[] { Vector2.up, Vector2.down, Vector2.left, Vector2.right });
    }

    private void SearchEmptyTile ()
    {
        switch (_gravityChganger.GetDirection ())
        {
            case GravityDirection.RIGHT:
            case GravityDirection.LEFT:
                {
                    SerchEmptyHorizontalTile ();
                    break;
                }
            default:
                {
                    SerchEmptyVerticalTile ();
                    break;
                }
        }
    }

    private void SerchEmptyHorizontalTile ()
    {
        for (int y = 0; y < _ySize; y++)
        {
            for (int x = 0; x < _xSize; x++)
            {
                if (_tiles[x, y].isEmpty)
                {
                    ChooseShiftDirection (x, y);
                    break;
                }
            }
        }
    }

    private void SerchEmptyVerticalTile ()
    {
        for (int x = 0; x < _xSize; x++)
        {
            for (int y = 0; y < _ySize; y++)
            {
                if (_tiles[x, y].isEmpty)
                {
                    ChooseShiftDirection (x, y);
                    break;
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

    #region DownGravity
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
        if (_useBlocks)
        {
            CheckBlocksDown (xPos);
        }
    }

    private void CheckBlocksDown (int xPos)
    {
        for (int y = 0; y < _ySize; y++)
        {
            if (_tiles[xPos, y].spriteRenderer.sprite == _blockSprite)
            {
                if (!_tiles[xPos, y].isBlock)
                {
                    ShiftBlocksDown (xPos, y);
                    break;
                }
            }
        }
    }

    private void ShiftBlocksDown (int xPos, int yPos)
    {
        List<Sprite> cashRenderer = new List<Sprite> ();
        for (int y = yPos; y < _ySize; y++)
        {
            Tile tile = _tiles[xPos, y];
            if (!tile.isEmpty)
            {
                cashRenderer.Add (tile.spriteRenderer.sprite);
            }
        }
        SetNewSpriteAfterBlockDown (xPos, yPos, cashRenderer);
    }

    private void SetNewSpriteAfterBlockDown (int xPos, int yPos, List<Sprite> renderers)
    {
        int yStartPosition = 0;
        for (int y = yPos; y < _ySize; y++)
        {
            if (_tiles[xPos, y].isBlock)
            {
                yStartPosition = _tiles[xPos, y].yPosition;
                break;
            }
        }
        for (int y = yStartPosition; y < yStartPosition + renderers.Count; y++)
        {
            _tiles[xPos, y].spriteRenderer.sprite = renderers[y - yStartPosition];
        }
        for (int y = yPos; y < yStartPosition; y++)
        {
            _tiles[xPos, y].spriteRenderer.sprite = null;
        }
    }
    #endregion

    #region UpGravity
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
        if (_useBlocks)
        {
            CheckBlocksUp (xPos);
        }
    }

    private void CheckBlocksUp (int xPos)
    {
        for (int y = _ySize - 1; y > -1; y--)
        {
            if (_tiles[xPos, y].spriteRenderer.sprite == _blockSprite)
            {
                if (!_tiles[xPos, y].isBlock)
                {
                    ShiftBlocksUp (xPos, y);
                    break;
                }
            }
        }
    }

    private void ShiftBlocksUp (int xPos, int yPos)
    {
        List<Sprite> cashRenderer = new List<Sprite> ();
        for (int y = yPos; y > -1; y--)
        {
            Tile tile = _tiles[xPos, y];
            if (!tile.isEmpty)
            {
                cashRenderer.Add (tile.spriteRenderer.sprite);
            }
        }
        SetNewSpriteAfterBlockUp (xPos, yPos, cashRenderer);
    }

    private void SetNewSpriteAfterBlockUp (int xPos, int yPos, List<Sprite> renderers)
    {
        int yStartPosition = 0;
        for (int y = yPos; y > -1; y--)
        {
            if (_tiles[xPos, y].isBlock)
            {
                yStartPosition = _tiles[xPos, y].yPosition;
                break;
            }
        }
        for (int y = yStartPosition; y > yStartPosition - renderers.Count; y--)
        {
            _tiles[xPos, y].spriteRenderer.sprite = renderers[yStartPosition - y];
        }
        for (int y = yPos; y > yStartPosition; y--)
        {
            _tiles[xPos, y].spriteRenderer.sprite = null;
        }
    }
    #endregion

    #region RightGravity
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
        if (_useBlocks)
        {
            CheckBlocksRight (yPos);
        }
    }

    private void CheckBlocksRight (int yPos)
    {
        for (int x = _xSize - 1; x > -1; x--)
        {
            if (_tiles[x, yPos].spriteRenderer.sprite == _blockSprite)
            {
                if (!_tiles[x, yPos].isBlock)
                {
                    ShiftBlocksRight (x, yPos);
                    break;
                }
            }
        }
    }

    private void ShiftBlocksRight (int xPos, int yPos)
    {
        List<Sprite> cashRenderer = new List<Sprite> ();
        for (int x = xPos; x > -1; x--)
        {
            Tile tile = _tiles[x, yPos];
            if (!tile.isEmpty)
            {
                cashRenderer.Add (tile.spriteRenderer.sprite);
            }
        }
        SetNewSpriteAfterBlockRight (xPos, yPos, cashRenderer);
    }

    private void SetNewSpriteAfterBlockRight (int xPos, int yPos, List<Sprite> renderers)
    {
        int xStartPosition = 0;
        for (int x = xPos; x > -1; x--)
        {
            if (_tiles[x, yPos].isBlock)
            {
                xStartPosition = _tiles[x, yPos].xPosition;
                break;
            }
        }
        for (int x = xStartPosition; x > xStartPosition - renderers.Count; x--)
        {
            _tiles[x, yPos].spriteRenderer.sprite = renderers[xStartPosition - x];
        }
        for (int x = xPos; x > xStartPosition; x--)
        {
            _tiles[x, yPos].spriteRenderer.sprite = null;
        }
    }

    #endregion

    #region LeftGravity
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
        if (_useBlocks)
        {
            CheckBlocksLeft (yPos);
        }
    }

    private void CheckBlocksLeft (int yPos)
    {
        for (int x = 0; x < _xSize; x++)
        {
            if (_tiles[x, yPos].spriteRenderer.sprite == _blockSprite)
            {
                if (!_tiles[x, yPos].isBlock)
                {
                    ShiftBlocksLeft (x, yPos);
                    break;
                }
            }
        }
    }

    private void ShiftBlocksLeft (int xPos, int yPos)
    {
        List<Sprite> cashRenderer = new List<Sprite> ();
        for (int x = xPos; x < _xSize; x++)
        {
            Tile tile = _tiles[x, yPos];
            if (!tile.isEmpty)
            {
                cashRenderer.Add (tile.spriteRenderer.sprite);
            }
        }
        SetNewSpriteAfterBlockLeft (xPos, yPos, cashRenderer);
    }

    private void SetNewSpriteAfterBlockLeft (int xPos, int yPos, List<Sprite> renderers)
    {
        int xStartPosition = 0;
        for (int x = xPos; x < _xSize; x++)
        {
            if (_tiles[x, yPos].isBlock)
            {
                xStartPosition = _tiles[x, yPos].xPosition;
                break;
            }
        }
        for (int x = xStartPosition; x < xStartPosition + renderers.Count; x++)
        {
            _tiles[x, yPos].spriteRenderer.sprite = renderers[x - xStartPosition];
        }
        for (int x = xPos; x < xStartPosition; x++)
        {
            _tiles[x, yPos].spriteRenderer.sprite = null;
        }
    }
    #endregion
}