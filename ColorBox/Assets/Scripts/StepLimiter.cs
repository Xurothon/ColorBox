using UnityEngine;
using UnityEngine.UI;

public class StepLimiter : MonoBehaviour
{
    [Range (0, 1)]
    [SerializeField] private float _percentageOfDimension;
    [SerializeField] private Text _countStepText;
    [SerializeField] private GameObject _restartLevelPanel;
    private int _countStep;
    private int _startCountStep;

    public void SetVelues (BoardSettings boardSettings)
    {
        _startCountStep = (int) (boardSettings.xSize * boardSettings.ySize * _percentageOfDimension);
        UpdateCountStep ();
    }

    public void MakeStep ()
    {
        _countStep--;
        UpdateCountTextStep ();
        if (_countStep <= 0) ActiveRestartPanel ();
    }

    public void ActiveRestartPanel ()
    {
        DataWorker.Instance.TakeAwayCrystal ();
        _restartLevelPanel.SetActive (true);
    }

    public void UpdateCountTextStep ()
    {
        _countStepText.text = _countStep.ToString ();
    }

    public void UpdateCountStep ()
    {
        _countStep = _startCountStep;
        UpdateCountTextStep ();
    }
}