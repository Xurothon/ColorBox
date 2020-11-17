using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBoardController : MonoBehaviour
{
    [SerializeField] private TutorialGravityChanger _gravityChganger;
    private int _xSize, _ySize;
    private List<Sprite> _tileSprites = new List<Sprite> ();
    private Tile[, ] _tiles;
    private Vector2[] dirRay = new Vector2[] { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
    private bool _isFindMatch;
    private bool _isFindMatchAgain;
    private bool _isLevelComplete;
    private bool _isStopFindMatch;

    public void SetValues (BoardSettings boardSettings, Tile[, ] tiles)
    {
        _xSize = boardSettings.xSize;
        _ySize = boardSettings.ySize;
        _tileSprites = boardSettings.tileSprites;
        _tiles = tiles;
    }

    public void SwapTwoTiles (TutorialMainTile mainTile, Tile tile)
    {
        if (!tile.isEmpty && !tile.isPlayer)
        {
            Sprite cashSprite = mainTile.image.sprite;
            mainTile.image.sprite = tile.spriteRenderer.sprite;
            tile.spriteRenderer.sprite = cashSprite;
            FindAllMatch (tile);
            TutorialGameHelper.Instance.StartTutorial2 ();
        }
    }

    private void CheckLevelComplete ()
    {
        _isLevelComplete = true;
        // for (int x = 0; x < _xSize; x++)
        // {
        //     for (int y = 0; y < _ySize; y++)
        //     {
        //         if (_tiles[x, y].spriteRenderer.sprite != null)
        //         {
        //             _isLevelComplete = false;
        //         }
        //     }
        // }
        if (!_tiles[0, 0].isPlayer) _isLevelComplete = false;
        if (_isLevelComplete)
        {
            DataWorker.Instance.AddLevelCompleteCount ();
            GameUIHelper.Instance.ShowLevelCompletePanel ();
        }
    }

    private void Start ()
    {
        _gravityChganger.OnGravityChange.AddListener (SearchEmptyTile);
        _gravityChganger.OnGravityChange.AddListener (TutorialGameHelper.Instance.FinishTutorial2);
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
            StartCoroutine (DeleteSprites (tile, cashFindSprite));
            _isFindMatch = true;
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
        DeleteSprite (tile, new Vector2[] { Vector2.up, Vector2.left, Vector2.down, Vector2.right });
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
        FindAllMatchAterGravityChange ();
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