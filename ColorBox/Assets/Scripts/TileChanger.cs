using UnityEngine;

public class TileChanger : MonoBehaviour
{
    [SerializeField] private BonusPriceList _bonusPriceList;
    [SerializeField] private GameObject _tileChangePanel;
    private Tile _cashTile;

    public void MyOnPointerUp ()
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
            if (hitLeft.collider.gameObject.GetComponent<Tile> ().spriteRenderer.sprite != null)
            {
                _cashTile = hitLeft.collider.gameObject.GetComponent<Tile> ();
                _bonusPriceList.IsBuyTileChange ();
            }
        }
        _tileChangePanel.SetActive (false);
    }

    public void ActiveChanger ()
    {
        _tileChangePanel.SetActive (true);
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