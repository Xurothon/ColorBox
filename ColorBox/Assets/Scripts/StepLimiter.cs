using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StepLimiter : MonoBehaviour
{
    [Range (0, 1)]
    [SerializeField] private float _percentageOfDimension;
    [SerializeField] private Text _countStepText;
    [SerializeField] private int _waitSecondForRestartPanel;
    [SerializeField] private int _stepCountAfterVideo;
    [SerializeField] private Text _stepCountAfterVideText;
    [SerializeField] private GameObject _restartLevelButtonsPanel;
    [SerializeField] private GameObject _restartLevelPanel;
    [SerializeField] private BonusPriceList _bonusPriceList;
    private int _countStep;
    private int _startCountStep;

    public void SetVelues (BoardSettings boardSettings)
    {
        _startCountStep = (int) (boardSettings.xSize * boardSettings.ySize * _percentageOfDimension);
        _stepCountAfterVideText.text = "+" + _stepCountAfterVideo;
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
        _restartLevelButtonsPanel.SetActive (false);
        _restartLevelPanel.SetActive (true);
        StartCoroutine (WaitSomeSecond ());
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

    public void AddSteps (int countStep)
    {
        _countStep += countStep;
        UpdateCountTextStep ();
    }

    public void AddStepLimit ()
    {
        if (_bonusPriceList.IsBuyAddStepLimit ())
        {
            AddStepLimitAfterVideo ();
        }
    }

    public void AddStepLimitAfterVideo ()
    {
        AddSteps (_bonusPriceList.AddStepLimit);
    }

    public void AddStepLimitAfterVideoRestart ()
    {
        AddSteps (_stepCountAfterVideo);
    }

    private IEnumerator WaitSomeSecond ()
    {
        yield return new WaitForSeconds (_waitSecondForRestartPanel);
        if (_restartLevelPanel.active)
        {
            DataWorker.Instance.TakeAwayCrystal ();
            _restartLevelButtonsPanel.SetActive (true);
        }
    }

    private void OnDisable ()
    {
        StopAllCoroutines ();
    }
}