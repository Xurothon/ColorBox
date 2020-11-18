using UnityEngine;

[System.Serializable]
public class MyIntEvent : UnityEngine.Events.UnityEvent<int> { }
public class DataWorker : MonoBehaviour
{
    public static DataWorker Instance;
    public int crystal { get; private set; }
    public int isBuyAdsOff { get; private set; }
    public int levelCompleteCount { get; private set; }
    private int _crystalInLevel;
    private bool _isTakeAwayCrystal;

    [HideInInspector] public MyIntEvent OnValueChangeCrystal;

    public void AddCrystal (int value)
    {
        _crystalInLevel += value;
        crystal += value;
        OnValueChangeCrystal?.Invoke (crystal);
    }

    public void GetDoubleCrystal ()
    {
        crystal += _crystalInLevel;
        OnValueChangeCrystal?.Invoke (crystal);
    }

    public void DeductCrystal (int value)
    {
        crystal -= value;
        OnValueChangeCrystal?.Invoke (crystal);
    }
    public void BuyAdsOff ()
    {
        isBuyAdsOff = 1;
    }

    public void AddLevelCompleteCount ()
    {
        levelCompleteCount++;
    }

    public void ResetLevelCompleteCount ()
    {
        levelCompleteCount = 0;
    }

    public void TakeAwayCrystal ()
    {
        if (!_isTakeAwayCrystal)
        {
            crystal -= _crystalInLevel;
            _isTakeAwayCrystal = true;
        }
    }

    private void Awake ()
    {
        Instance = this;
        ReadAllPlayerPrefs ();
    }

    private void ReadAllPlayerPrefs ()
    {
        crystal = GetValue (PlayerPrefsKeys.CURRENT_CRYSTAL.ToString ());
        isBuyAdsOff = GetValue (PlayerPrefsKeys.IS_BUY_ADS_OFF.ToString ());
        levelCompleteCount = GetValue (PlayerPrefsKeys.LEVEL_COMPLETE_COUNT.ToString ());
    }

    private int GetValue (string playerPrefsKey)
    {
        if (PlayerPrefs.HasKey (playerPrefsKey))
        {
            return PlayerPrefs.GetInt (playerPrefsKey);
        }
        return 0;
    }

    private void SaveValue (string playerPrefsKey, int value)
    {
        PlayerPrefs.SetInt (playerPrefsKey, value);
    }

    private void OnDisable ()
    {
        SaveValue (PlayerPrefsKeys.CURRENT_CRYSTAL.ToString (), crystal);
        SaveValue (PlayerPrefsKeys.IS_BUY_ADS_OFF.ToString (), isBuyAdsOff);
        SaveValue (PlayerPrefsKeys.LEVEL_COMPLETE_COUNT.ToString (), levelCompleteCount);
        OnValueChangeCrystal.RemoveAllListeners ();
    }
}