using UnityEngine;

public class GravityChanger : MonoBehaviour
{

    [HideInInspector] public UnityEngine.Events.UnityEvent OnGravityChange;
    [SerializeField] private GravityStore[] _gravityStore;
    [SerializeField] private UnityEngine.UI.Image _buttonImage;
    private int _currentGravityStoreIndex;

    public void ChangeGravityDirection ()
    {
        if (!GameHelper.Instance.IsSearchEmptyTile)
        {
            SoundsHelper.Instance.PlayGravitationClip ();
            _currentGravityStoreIndex++;
            if (_currentGravityStoreIndex == _gravityStore.Length) _currentGravityStoreIndex = 0;
            ChangeGravityImage ();
            OnGravityChange?.Invoke ();
        }
    }

    public GravityDirection GetDirection ()
    {
        return _gravityStore[_currentGravityStoreIndex].gravityDirection;
    }

    private void Start ()
    {
        ChangeGravityImage ();
    }

    private void ChangeGravityImage ()
    {
        _buttonImage.sprite = _gravityStore[_currentGravityStoreIndex].gravityImage;
    }

    private void OnDisable ()
    {
        OnGravityChange.RemoveAllListeners ();
    }
}

[System.Serializable]
public class GravityStore
{
    public GravityDirection gravityDirection;
    public Sprite gravityImage;
}