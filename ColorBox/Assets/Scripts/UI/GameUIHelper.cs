using UnityEngine;

public class GameUIHelper : MonoBehaviour
{
    public static GameUIHelper Instance;
    [SerializeField] private GameObject _videoStepCanselPanel;
    [SerializeField] private GameObject _videoTileChangePanel;
    [SerializeField] private GameObject _levelCompletePanel;
    [SerializeField] private GameObject _tileChangeChoosePanel;

    private void Awake ()
    {
        Instance = this;
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

    public void ShowTileChangePanel ()
    {
        _tileChangeChoosePanel.SetActive (true);
    }

    public void HideTileChangePanel ()
    {
        _tileChangeChoosePanel.SetActive (false);

    }
}