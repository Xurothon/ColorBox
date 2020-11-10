using UnityEngine;

public class GameHelper : MonoBehaviour
{
    public static GameHelper Instance;
    public BoardSettings boardSettings;
    [SerializeField] private BoardCreator _borderCreator;
    [SerializeField] private BoardController _borderController;
    [SerializeField] private TileChanger _tileChanger;
    public GravityDirection gravityDirection;
    private bool _isSearchEmptyTile;
    public bool IsSearchEmptyTile { get { return _isSearchEmptyTile; } }

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
        _borderController.ChangeSpriteTile (tile, sprite);
    }

    public void LoadMainScene ()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene (0);
    }

    public void LoadNextScene ()
    {
        int currentScene = UnityEngine.SceneManagement.SceneManager.sceneCount;
        currentScene++;
        if (currentScene > UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings - 1) currentScene = 0;
        UnityEngine.SceneManagement.SceneManager.LoadScene (currentScene);
    }

    public void DisableCileChanger ()
    {
        _tileChanger.DisableChanger ();
    }
    public void StartSearchEmptyTile ()
    {
        _isSearchEmptyTile = true;
    }

    public void EndSearchEmptyTile ()
    {
        _isSearchEmptyTile = false;
    }
}