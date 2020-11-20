using UnityEngine;

public class GameUIHelper : MonoBehaviour
{
    public static GameUIHelper Instance;
    [SerializeField] private GameObject _videoStepCanselPanel;
    [SerializeField] private GameObject _videoTileChangePanel;
    [SerializeField] private GameObject _videoAddStepLimitPanel;
    [SerializeField] private GameObject _videoDeleteSpritePanel;
    [SerializeField] private GameObject _levelCompletePanel;
    [SerializeField] private GameObject _tileChangeChoosePanel;
    [SerializeField] private UnityEngine.UI.Text _currentLevelText;

    private void Awake ()
    {
        Instance = this;
        _currentLevelText.text = "level " + UnityEngine.SceneManagement.SceneManager.GetActiveScene ().name;
    }

    public void ShowVideoStepCancelPanel ()
    {
        _videoStepCanselPanel.SetActive (true);
    }

    public void ShowLevelCompletePanel ()
    {
        _levelCompletePanel.SetActive (true);
    }

    public void ShowVideoTileChangePanel ()
    {
        _videoTileChangePanel.SetActive (true);
    }

    public void ShowVideoAddStepLimitPanel ()
    {
        _videoAddStepLimitPanel.SetActive (true);
    }

    public void ShowVideoDeleteSpritePanel ()
    {
        _videoDeleteSpritePanel.SetActive (true);
    }

    public void ShowTileChangePanel ()
    {
        _tileChangeChoosePanel.SetActive (true);
    }

    public void HideTileChangePanel ()
    {
        _tileChangeChoosePanel.SetActive (false);

    }
}