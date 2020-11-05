using UnityEngine;

[System.Serializable]
public class MyIntEvent : UnityEngine.Events.UnityEvent<int> { }
public class DataWorker : MonoBehaviour
{
    public static DataWorker Instance;
    public int Crystal { get; private set; }

    [HideInInspector] public MyIntEvent OnValueChangeCrystal;

    public void AddCrystal (int value)
    {
        Crystal += value;
        OnValueChangeCrystal?.Invoke (Crystal);
    }

    public void DeductCrystal (int value)
    {
        Crystal -= value;
        OnValueChangeCrystal?.Invoke (Crystal);
    }

    private void Awake ()
    {
        Instance = this;
        ReadAllPlayerPrefs ();
    }

    private void ReadAllPlayerPrefs ()
    {
        Crystal = GetValue (PlayerPrefsKeys.CURRENT_CRYSTAL.ToString ());
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
        SaveValue (PlayerPrefsKeys.CURRENT_CRYSTAL.ToString (), Crystal);
        OnValueChangeCrystal.RemoveAllListeners ();
    }
}