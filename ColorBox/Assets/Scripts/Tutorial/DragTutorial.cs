using DG.Tweening;
using UnityEngine;

public class DragTutorial : MonoBehaviour
{
    [SerializeField] private Vector3 _touchScalePointer;
    [SerializeField] private float _durationScale;
    [SerializeField] private Transform _pointer1;
    [SerializeField] private Transform _startPositionPoinet1;
    [SerializeField] private Transform _endPositionPoinet1;
    [SerializeField] private float _durationSpeed;
    private Sequence _mySequence;

    private void Start ()
    {
        _mySequence = DOTween.Sequence ();
        StartTutorial ();
    }

    public void StartTutorial ()
    {
        _pointer1.position = _startPositionPoinet1.position;
        _mySequence.Append (_pointer1.DOScale (_touchScalePointer, _durationScale))
            .Append (_pointer1.DOMove (_endPositionPoinet1.position, _durationSpeed))
            .Append (_pointer1.DOScale (new Vector3 (1f, 1f, 1f), _durationScale).OnComplete (RepeatTutorial));;
    }

    public void RepeatTutorial ()
    {
        _pointer1.position = _startPositionPoinet1.position;
        _mySequence.Restart ();
    }

    public void FinishTutorial ()
    {
        _mySequence.Kill ();
        _pointer1.gameObject.SetActive (false);
    }
}