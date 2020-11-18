using System;
using UnityEngine;

public class MainUIHelper : MonoBehaviour
{
    [SerializeField] private GameObject _adsOffPanel;
    [SerializeField] private GameObject _adsPanel;

    private void Start ()
    {
        CheckLevelCompleteCount ();
    }

    public void CheckLevelCompleteCount ()
    {
        if (DataWorker.Instance.levelCompleteCount == 3)
        {
            _adsPanel.SetActive (true);
            DataWorker.Instance.ResetLevelCompleteCount ();
        }
    }

    public void DeactivePanel (GameObject panel)
    {
        panel.SetActive (false);
    }

    public void ActivePanel (GameObject panel)
    {
        panel.SetActive (true);
    }

    public void LoadScene (int idScene)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene (idScene);
    }

    public void BuyAdsOffPanel ()
    {
        if (DataWorker.Instance.isBuyAdsOff == 0)
        {
            _adsOffPanel.SetActive (true);
            DataWorker.Instance.BuyAdsOff ();
        }
    }

}