using DG.Tweening;
using UnityEngine;

public class GravityTutorial : MonoBehaviour
{
    [SerializeField] private Vector3 _touchScalePointer;
    [SerializeField] private float _durationScale;
    [SerializeField] private Transform _pointer2;
    [SerializeField] private Transform _startPositionPointer2;
    [SerializeField] private Transform _endPositionPointer2;
    [SerializeField] private float _durationSpeed;
    private Sequence _mySequence;
    private bool _isTutorial2Complete;

    public void StartTutorial ()
    {
        if (!_isTutorial2Complete)
        {
            _mySequence = DOTween.Sequence ();
            StartTut ();
        }
    }

    public void StartTut ()
    {
        _pointer2.gameObject.SetActive (true);
        _pointer2.position = _startPositionPointer2.position;
        _mySequence.Append (_pointer2.DOScale (_touchScalePointer, _durationScale))
            .Append (_pointer2.DOMove (_endPositionPointer2.position, _durationSpeed))
            .Append (_pointer2.DOScale (new Vector3 (1f, 1f, 1f), _durationScale).OnComplete (RepeatTutorial2));
    }

    public void RepeatTutorial2 ()
    {
        _pointer2.position = _startPositionPointer2.position;
        _mySequence.Restart ();
    }

    public void FinishTutorial ()
    {
        if (!_isTutorial2Complete)
        {
            _mySequence.Kill ();
            _pointer2.gameObject.SetActive (false);
            _isTutorial2Complete = true;
        }
    }
}