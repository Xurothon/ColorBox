using UnityEngine;

public class GameHelper : MonoBehaviour
{
    public static GameHelper Instance;
    public BoardSettings boardSettings;
    [SerializeField] private BoardCreator _borderCreator;
    [SerializeField] private BoardController _borderController;
    [SerializeField] private TileChanger _tileChanger;
    public GravityDirection gravityDirection;

    private void Awake ()
    {
        Instance = this;
    }

    private void Start ()
    {
        Tile[, ] tileArray = _borderCreator.SetValues (boardSettings);
        _borderController.SetValues (boardSettings, tileArray);
    }

    public void SwapTwoTiles (MainTile mainTile, Tile tile)
    {
        _borderController.SwapTwoTiles (mainTile, tile);
    }

    public void ChangeSpriteTile (Tile tile, Sprite sprite)
    {
        _borderController.ChageSpriteTile (tile, sprite);
    }

    public void LoadScene ()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene (0);
    }

    public void DisableCileChanger ()
    {
        _tileChanger.DisableChanger ();
    }
}