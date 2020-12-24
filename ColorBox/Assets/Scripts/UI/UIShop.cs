using UnityEngine;

public class UIShop : MonoBehaviour
{
    [SerializeField] private ShopButton _startShopButton;
    private ShopButton _currentShopButton;

    private void OnEnable()
    {
        ActiveShop();
    }

    private void ActiveShop()
    {
        _startShopButton.ActivePanel();
        _currentShopButton = _startShopButton;
    }

    public void ActivePanel(ShopButton shopButton)
    {
        _currentShopButton.DisablePanel();
        shopButton.ActivePanel();
        _currentShopButton = shopButton;
    }

    public void Disable()
    {
        _currentShopButton.DisablePanel();
        gameObject.SetActive(false);
    }
}
