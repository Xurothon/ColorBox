using UnityEngine;

public class UICrystalChanger : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Text _crystalText;

    private void Start ()
    {
        DataWorker.Instance.OnValueChangeCrystal.AddListener (ChangeCrystalText);
        _crystalText.text = "x" + DataWorker.Instance.crystal;
    }

    public void ChangeCrystalText (int crystalCount)
    {
        _crystalText.text = "x" + crystalCount;
    }
}