using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour
{
    [SerializeField] private GravityChanger _gravityChganger;
    [SerializeField] private StepCancel _stepCancel;
    [SerializeField] private StepLimiter _stepLimiter;
    private int _xSize, _ySize;
    private Tile[, ] _tiles;
    private bool _isLevelComplete;
    private bool _useBlocks;
    private bool _isFindMatchAgain;
    private bool _isStopFindMatch;
    private Sprite _blockSprite;

    public void SetValues (BoardSettings boardSettings, Tile[, ] tiles)
    {
        _xSize = boardSettings.xSize;
        _ySize = boardSettings.ySize;
        _tiles = tiles;
        _useBlocks = boardSettings.useBlocks;
        _blockSprite = boardSettings.blockSprite;
        _stepCancel.SetValues (boardSettings, tiles);
        _stepLimiter.SetVelues (boardSettings);
    }

    public void SwapTwoTiles (MainTile mainTile, Tile tile)
    {
        if (!tile.isEmpty)
        {
            if (!tile.isBlock && !tile.isPlayer && !tile.isEnemy)
            {
                _stepLimiter.MakeStep ();
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
        if (!_tiles[0, 0].isPlayer) _isLevelComplete = false;
        if (_isLevelComplete)
        {
            DataWorker.Instance.AddLevelCompleteCount ();
            GameUIHelper.Instance.ShowLevelCompletePanel ();
        }
    }

    private void Start ()
    {
        _gravityChganger.OnGravityChange.AddListener (CallAllMethodAfterGravityChange);
    }

    private void CallAllMethodAfterGravityChange ()
    {
        _stepCancel.SavePreviosStep ();
        SearchEmptyTile ();
        _stepLimiter.MakeStep ();
    }

    private List<Tile> FindMatch (Tile tile, Vector2 dir)
    {

        List<Tile> cashFindTile = new List<Tile> ();
        RaycastHit2D hit = Physics2D.Raycast (tile.transform.position, dir);
        switch (dir.y)
        {
            case 0:
                {
                    while (hit.collider != null && hit.collider.gameObject.GetComponent<Tile> ().spriteRenderer.sprite == tile.spriteRenderer.sprite)
                    {
                        Tile tempTile = hit.collider.gameObject.GetComponent<Tile> ();
                        if (!cashFindTile.Contains (tempTile))
                        {
                            FindNeighborsMatch (tempTile, new Vector2[] { Vector2.up, Vector2.down }, cashFindTile);
                            cashFindTile.Add (hit.collider.gameObject.GetComponent<Tile> ());
                            hit = Physics2D.Raycast (hit.collider.transform.position, dir);
                        }
                    }
                    break;
                }
            default:
                while (hit.collider != null && hit.collider.gameObject.GetComponent<Tile> ().spriteRenderer.sprite == tile.spriteRenderer.sprite)
                {
                    Tile tempTile = hit.collider.gameObject.GetComponent<Tile> ();
                    if (!cashFindTile.Contains (tempTile))
                    {
                        FindNeighborsMatch (tempTile, new Vector2[] { Vector2.left, Vector2.right }, cashFindTile);
                        cashFindTile.Add (hit.collider.gameObject.GetComponent<Tile> ());
                        hit = Physics2D.Raycast (hit.collider.transform.position, dir);
                    }
                }
                break;
        }
        return cashFindTile;
    }

    private List<Tile> FindNeighborsMatch (Tile tile, Vector2[] dir, List<Tile> cashFindTile)
    {
        for (int i = 0; i < dir.Length; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast (tile.transform.position, dir[i]);
            while (hit.collider != null && hit.collider.gameObject.GetComponent<Tile> ().spriteRenderer.sprite == tile.spriteRenderer.sprite)
            {
                Tile tempTile = hit.collider.gameObject.GetComponent<Tile> ();
                if (!cashFindTile.Contains (tempTile))
                {
                    cashFindTile.Add (hit.collider.gameObject.GetComponent<Tile> ());
                    hit = Physics2D.Raycast (hit.collider.transform.position, dir[i]);
                }
            }
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
            _isFindMatchAgain = true;
            if (cashFindSprite.Count > 3)
            {
                DataWorker.Instance.AddCrystal (1);
            }
            StartCoroutine (DeleteSprites (tile, cashFindSprite));
        }
    }

    private IEnumerator DeleteSprites (Tile tile, List<Tile> cashTiles)
    {
        tile.spriteRenderer.sprite = null;
        yield return new WaitForSeconds (0.1f);
        for (int i = 0; i < cashTiles.Count; i++)
        {
            SoundsHelper.Instance.PlayDisappearanceTile ();
            cashTiles[i].spriteRenderer.sprite = null;
            yield return new WaitForSeconds (0.1f);
        }
        SearchEmptyTile ();
        CheckLevelComplete ();
    }

    private void FindAllMatchAterGravityChange ()
    {
        _isStopFindMatch = false;
        while (!_isStopFindMatch)
            FindAllMatchLoop ();
    }

    private void FindAllMatchLoop ()
    {
        _isFindMatchAgain = false;
        for (int x = 0; x < _xSize; x++)
        {
            for (int y = 0; y < _ySize; y++)
            {
                FindAllMatch (_tiles[x, y]);
                if (_isFindMatchAgain)
                {
                    return;
                }
            }
            if (x == _xSize - 1)
            {
                _isStopFindMatch = true;
            }
        }
    }

    private void FindAllMatch (Tile tile)
    {
        if (tile.isEmpty) return;
        if (tile.isBlock) return;
        if (tile.isEnemy) return;
        DeleteSprite (tile, new Vector2[] { Vector2.up, Vector2.left, Vector2.down, Vector2.right });
        CheckEnemyBeside ();
    }

    private void SearchEmptyTile ()
    {
        GameHelper.Instance.StartSearchEmptyTile ();
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
        FindAllMatchAterGravityChange ();
        GameHelper.Instance.EndSearchEmptyTile ();
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
        CheckLevelComplete ();
    }

    #region DownGravity
    private void ShiftTileDown (int xPos, int yPos)
    {
        List<Sprite> cashRenderer = new List<Sprite> ();
        for (int y = 0; y < _ySize; y++)
        {
            Tile tile = _tiles[xPos, y];
            if (!tile.isEmpty)
            {
                cashRenderer.Add (tile.spriteRenderer.sprite);
            }
        }
        SetNewSpriteDown (xPos, yPos, cashRenderer);
    }
    private void SetNewSpriteDown (int xPos, int yPos, List<Sprite> renderers)
    {
        int yEndPos = renderers.Count;
        for (int y = 0; y < yEndPos; y++)
        {
            _tiles[xPos, y].spriteRenderer.sprite = renderers[y];
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
        List<Sprite> cashRenderer = new List<Sprite> ();
        for (int y = _ySize - 1; y > -1; y--)
        {
            Tile tile = _tiles[xPos, y];
            if (!tile.isEmpty)
            {
                cashRenderer.Add (tile.spriteRenderer.sprite);
            }
        }
        SetNewSpriteUp (xPos, yPos, cashRenderer);
    }
    private void SetNewSpriteUp (int xPos, int yPos, List<Sprite> renderers)
    {
        int yEndPos = _ySize - renderers.Count - 1;
        for (int y = _ySize - 1; y > yEndPos; y--)
        {
            _tiles[xPos, y].spriteRenderer.sprite = renderers[_ySize - 1 - y];
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
        List<Sprite> cashRenderer = new List<Sprite> ();
        for (int x = _xSize - 1; x > -1; x--)
        {
            Tile tile = _tiles[x, yPos];
            if (!tile.isEmpty)
            {
                cashRenderer.Add (tile.spriteRenderer.sprite);
            }
        }
        SetNewSpriteRight (xPos, yPos, cashRenderer);
    }

    private void SetNewSpriteRight (int xPos, int yPos, List<Sprite> renderers)
    {
        int xEndPos = _xSize - renderers.Count - 1;
        for (int x = _xSize - 1; x > xEndPos; x--)
        {
            _tiles[x, yPos].spriteRenderer.sprite = renderers[_xSize - 1 - x];
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
        List<Sprite> cashRenderer = new List<Sprite> ();
        for (int x = 0; x < _xSize; x++)
        {
            Tile tile = _tiles[x, yPos];
            if (!tile.isEmpty)
            {
                cashRenderer.Add (tile.spriteRenderer.sprite);
            }
        }
        SetNewSpriteLeft (xPos, yPos, cashRenderer);
    }

    private void SetNewSpriteLeft (int xPos, int yPos, List<Sprite> renderers)
    {
        int xEndPos = renderers.Count;
        for (int x = 0; x < xEndPos; x++)
        {
            _tiles[x, yPos].spriteRenderer.sprite = renderers[x];
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

    private void CheckEnemyBeside ()
    {
        Tile player = FindPlayer ();
        bool isEnemyBeside = false;
        if (player != null)
        {
            Vector2[] dir = new Vector2[] { Vector2.up, Vector2.down, Vector2.right, Vector2.left };
            for (int i = 0; i < dir.Length; i++)
            {
                if (!isEnemyBeside)
                {
                    RaycastHit2D hit = Physics2D.Raycast (player.transform.position, dir[i]);
                    Tile tempTile = GetTile (hit);
                    if (tempTile != null && tempTile.isEnemy)
                    {
                        isEnemyBeside = true;
                    }
                }
            }
        }
        if (isEnemyBeside) _stepLimiter.ActiveRestartPanel ();
    }

    private Tile FindPlayer ()
    {
        for (int x = 0; x < _xSize; x++)
        {
            for (int y = 0; y < _ySize; y++)
            {
                if (_tiles[x, y].isPlayer)
                {
                    return _tiles[x, y];
                }
            }
        }
        return null;
    }

    private Tile GetTile (RaycastHit2D rayHit)
    {
        Tile tile = null;
        try
        {
            tile = rayHit.collider.gameObject.GetComponent<Tile> ();
        }
        catch { }
        return tile;
    }
}