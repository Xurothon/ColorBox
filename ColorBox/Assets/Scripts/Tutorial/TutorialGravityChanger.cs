using UnityEngine;

public class TutorialGravityChanger : MonoBehaviour
{
    [HideInInspector] public UnityEngine.Events.UnityEvent OnGravityChange;
    [SerializeField] private GravityStore[] _gravityStore;
    [SerializeField] private Camera _mainCamera;
    private int _currentGravityStoreIndex;
    private Vector2 _startTouchPosition;
    private Vector2 _endTouchPosition;

    public void MyOnMouseDown ()
    {
        _startTouchPosition = _mainCamera.ScreenToViewportPoint (Input.mousePosition);
    }

    public void MyOnMouseUp ()
    {
        _endTouchPosition = _mainCamera.ScreenToViewportPoint (Input.mousePosition);
        Vector2 direction = _endTouchPosition - _startTouchPosition;
        if (Mathf.Abs (direction.x) > Mathf.Abs (direction.y))
        {
            if (direction.x > 0)
            {
                _currentGravityStoreIndex = (int) GravityDirection.RIGHT;
            }
            else
            {
                _currentGravityStoreIndex = (int) GravityDirection.LEFT;
            }
        }
        else
        {
            if (direction.y > 0)
            {
                _currentGravityStoreIndex = (int) GravityDirection.UP;
            }
            else
            {
                _currentGravityStoreIndex = (int) GravityDirection.DOWN;
            }
        }
        ChangeGravityDirection ();
    }

    public void ChangeGravityDirection ()
    {
        SoundsHelper.Instance.PlayGravitationClip ();
        ChangeGravityImage ();
        OnGravityChange?.Invoke ();
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
        DisabelAllSpriteRenderer ();
        _gravityStore[_currentGravityStoreIndex].spriteRenderer.enabled = true;
    }

    private void OnDisable ()
    {
        OnGravityChange.RemoveAllListeners ();
    }

    private void DisabelAllSpriteRenderer ()
    {
        for (var i = 0; i < _gravityStore.Length; i++)
        {
            _gravityStore[i].spriteRenderer.enabled = false;
        }
    }
}