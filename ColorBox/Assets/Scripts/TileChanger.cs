using UnityEngine;

public class TileChanger : MonoBehaviour
{
    [SerializeField] private BonusPriceList _bonusPriceList;
    private Tile _cashTile;
    private bool _isActive;
    public void MyOnPointerUp ()
    {
        if (_isActive)
        {
            Vector2 curMousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
            RaycastHit2D hitRight = Physics2D.Raycast (curMousePos, Vector2.right);
            Tile tile = null;
            try
            {
                tile = hitRight.collider.gameObject.GetComponent<Tile> ();
            }
            catch { }
            if (tile != null)
            {
                RaycastHit2D hitLeft = Physics2D.Raycast (hitRight.collider.transform.position, Vector2.left);
                _cashTile = hitLeft.collider.gameObject.GetComponent<Tile> ();
                _bonusPriceList.IsBuyTileChange ();
            }
            _isActive = false;
        }
    }

    public void ActiveChanger ()
    {
        _isActive = true;
    }

    public void DisableChanger ()
    {
        _isActive = false;
    }

    public void ChangeTileSprite (UnityEngine.UI.Image image)
    {
        GameUIHelper.Instance.HideTileChangePanel ();
        GameHelper.Instance.ChangeSpriteTile (_cashTile, image.sprite);
    }

    public void ChangeTileAfterVideo ()
    {
        GameUIHelper.Instance.ShowTileChangePanel ();
    }
}