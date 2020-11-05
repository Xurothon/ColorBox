using DG.Tweening;
using UnityEngine;

public class TutorialGameHelper : MonoBehaviour
{
    public static TutorialGameHelper Instance;
    public BoardSettings boardSettings;
    [SerializeField] private TutorialBoardCreator _borderCreator;
    [SerializeField] private TutorialBoardController _borderController;
    [SerializeField] private GravityDirection gravityDirection;
    [SerializeField] private DragTutorial _dragTutorial;
    [SerializeField] private GravityTutorial _garvityTutorial;

    private void Awake ()
    {
        Instance = this;
    }

    private void Start ()
    {
        Tile[, ] tileArray = _borderCreator.SetValues (boardSettings);
        _borderController.SetValues (boardSettings, tileArray);
        StartTutorial1 ();
    }

    public void SwapTwoTiles (TutorialMainTile mainTile, Tile tile)
    {
        _borderController.SwapTwoTiles (mainTile, tile);
    }

    public void LoadScene ()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene (0);
    }

    public void StartTutorial1 ()
    {
        _dragTutorial.StartTutorial ();
    }

    public void FinishTutorial1 ()
    {
        _dragTutorial.FinishTutorial ();
    }

    public void StartTutorial2 ()
    {
        _garvityTutorial.StartTutorial ();
    }

    public void FinishTutorial2 ()
    {
        _garvityTutorial.FinishTutorial ();
    }
}