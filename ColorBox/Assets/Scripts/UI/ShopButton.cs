using UnityEngine;

public class ShopButton : MonoBehaviour
{
    [SerializeField] private GameObject Panel;

    public void ActivePanel()
    {
        transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        Panel.SetActive(true);
    }

    public void DisablePanel()
    {
        transform.localScale = new Vector3(1f, 1f, 1f);
        Panel.SetActive(false);
    }
}
