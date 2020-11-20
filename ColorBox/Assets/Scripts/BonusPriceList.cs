using UnityEngine;

public class BonusPriceList : MonoBehaviour
{
    [SerializeField] private int _stepCancelCost;
    [SerializeField] private UnityEngine.UI.Text _stepCancelCostText;
    [SerializeField] private int _tileChangeCost;
    [SerializeField] private UnityEngine.UI.Text _changeTileCostText;
    [SerializeField] private int _addStepLimitCost;
    [SerializeField] private UnityEngine.UI.Text _addStepLimitCostText;
    [SerializeField] private int _deleteSpritreCost;
    [SerializeField] private UnityEngine.UI.Text _deleteSpritreCostText;
    [SerializeField] private int _addStepLimit;
    [SerializeField] private UnityEngine.UI.Text _addStepLimitText;
    public int AddStepLimit { get { return _addStepLimit; } }

    private void Start ()
    {
        _stepCancelCostText.text = _stepCancelCost.ToString ();
        _changeTileCostText.text = _tileChangeCost.ToString ();
        _addStepLimitCostText.text = _addStepLimitCost.ToString ();
        _deleteSpritreCostText.text = _deleteSpritreCost.ToString ();
        _addStepLimitText.text = "+" + _addStepLimit.ToString ();
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

    public bool IsBuyAddStepLimit ()
    {
        if (DataWorker.Instance.crystal >= _addStepLimitCost)
        {
            DataWorker.Instance.DeductCrystal (_addStepLimitCost);
            return true;
        }
        else
        {
            GameUIHelper.Instance.ShowVideoAddStepLimitPanel ();
            return false;
        }
    }

    public bool IsBuyDeleteSprite ()
    {
        if (DataWorker.Instance.crystal >= _deleteSpritreCost)
        {
            DataWorker.Instance.DeductCrystal (_deleteSpritreCost);
            return true;
        }
        else
        {
            GameUIHelper.Instance.ShowVideoDeleteSpritePanel ();
            return false;
        }
    }

}