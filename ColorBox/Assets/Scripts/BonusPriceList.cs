using UnityEngine;

public class BonusPriceList : MonoBehaviour
{
    [SerializeField] private int _stepCancelCost;
    [SerializeField] private UnityEngine.UI.Text _stepCancelCostText;
    [SerializeField] private int _tileChangeCost;
    [SerializeField] private UnityEngine.UI.Text _changeTileCostText;
    public int StepCancelCost { get { return _stepCancelCost; } }
    public int ChangeTileCost { get { return _tileChangeCost; } }

    private void Start ()
    {
        _stepCancelCostText.text = _stepCancelCost.ToString ();
        _changeTileCostText.text = _tileChangeCost.ToString ();
    }

    public bool IsBuyStepCancel ()
    {
        if (DataWorker.Instance.crystal >= _stepCancelCost)
        {
            DataWorker.Instance.DeductCrystal (_stepCancelCost);
            return true;
        }
        else
        {
            GameUIHelper.Instance.ShowVideoStepCancelPanel ();
            return false;
        }
    }

    public bool IsBuyTileChange ()
    {
        if (DataWorker.Instance.crystal >= _tileChangeCost)
        {
            DataWorker.Instance.DeductCrystal (_tileChangeCost);
            GameUIHelper.Instance.ShowTileChangePanel ();
            return true;
        }
        else
        {
            GameUIHelper.Instance.ShowVideoTileChangePanel ();
            return false;
        }
    }

}