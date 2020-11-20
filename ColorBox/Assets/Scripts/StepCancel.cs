using UnityEngine;

public class StepCancel : MonoBehaviour
{
    [SerializeField] private MainTile _mainTail;
    [SerializeField] private BonusPriceList _bonusPriceList;
    private Sprite[, ] _previousSprites;
    private Sprite _previousMainSprite;
    private Tile[, ] _tiles;
    private int _xSize, _ySize;
    private bool _isSave;

    public void SetValues (BoardSettings boardSettings, Tile[, ] tiles)
    {
        _xSize = boardSettings.xSize;
        _ySize = boardSettings.ySize;
        _tiles = tiles;
        _previousSprites = new Sprite[_xSize, _ySize];
    }

    public void SavePreviosStep ()
    {
        _previousMainSprite = _mainTail.image.sprite;
        for (int x = 0; x < _xSize; x++)
        {
            for (int y = 0; y < _ySize; y++)
            {
                _previousSprites[x, y] = _tiles[x, y].spriteRenderer.sprite;
            }
        }
        _isSave = true;
    }

    public void CancelStep ()
    {
        if (_isSave)
        {
            if (_bonusPriceList.IsBuyStepCancel ())
            {
                _mainTail.image.sprite = _previousMainSprite;
                for (int x = 0; x < _xSize; x++)
                {
                    for (int y = 0; y < _ySize; y++)
                    {
                        _tiles[x, y].spriteRenderer.sprite = _previousSprites[x, y];
                    }
                }
                _isSave = false;
            }
        }
    }

    public void ResetStartStep ()
    {
        int currentScene = int.Parse (UnityEngine.SceneManagement.SceneManager.GetActiveScene ().name);
        UnityEngine.SceneManagement.SceneManager.LoadScene (currentScene);
    }

    public void CancelStepAfterVideo ()
    {
        if (_isSave)
        {
            _mainTail.image.sprite = _previousMainSprite;
            for (int x = 0; x < _xSize; x++)
            {
                for (int y = 0; y < _ySize; y++)
                {
                    _tiles[x, y].spriteRenderer.sprite = _previousSprites[x, y];
                }
            }
            _isSave = false;
        }
    }
}