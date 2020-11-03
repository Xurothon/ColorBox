using UnityEngine;

public class UICrystalChanger : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Text _crystalText;

    private void Start ()
    {
        DataWorker.Instance.OnChangeCrystal.AddListener (ChangeCrystalText);
        _crystalText.text = "x" + DataWorker.Instance.Crystal;
    }

    public void ChangeCrystalText (int crystalCount)
    {
        _crystalText.text = "x" + crystalCount;
    }
}