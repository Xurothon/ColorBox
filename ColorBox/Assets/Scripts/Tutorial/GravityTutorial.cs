using DG.Tweening;
using UnityEngine;

public class GravityTutorial : MonoBehaviour
{
    [SerializeField] private Vector3 _touchScalePointer;
    [SerializeField] private float _durationScale;
    [SerializeField] private Transform _pointer2;
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
        _mySequence.Append (_pointer2.DOScale (_touchScalePointer, _durationScale))
            .Append (_pointer2.DOScale (new Vector3 (1f, 1f, 1f), _durationScale).OnComplete (RepeatTutorial2));
    }

    public void RepeatTutorial2 ()
    {
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