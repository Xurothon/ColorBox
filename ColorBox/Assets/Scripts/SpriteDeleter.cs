using UnityEngine;

public class SpriteDeleter : MonoBehaviour
{
    [SerializeField] private BonusPriceList _bonusPriceList;
    [SerializeField] private BoardController _boardController;
    [SerializeField] private GameObject _spriteDeletePanel;
    private Tile _cashTile;

    public void MyOnPointerUp ()
    {
        Vector2 curMousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
        RaycastHit2D hitRight = Physics2D.Raycast (curMousePos, Vector2.right);
        Tile tile = GetTile (hitRight);
        if (tile != null)
        {
            RaycastHit2D hitLeft = Physics2D.Raycast (hitRight.collider.transform.position, Vector2.left);
            Tile leftTile = GetTile (hitLeft);
            if (leftTile != null)
            {
                if (!leftTile.isBlock && !leftTile.isPlayer && !leftTile.isEnemy)
                {
                    _cashTile = leftTile;
                    DeleteSprite ();
                    _spriteDeletePanel.SetActive (false);
                }
            }
        }
    }
    public void DeleteSpriteAfterVideo ()
    {
        _boardController.DeleteOneSprite (_cashTile);
    }

    public void ActiveDeleter ()
    {
        _spriteDeletePanel.SetActive (true);
    }

    private void DeleteSprite ()
    {
        if (_bonusPriceList.IsBuyDeleteSprite ())
        {
            DeleteSpriteAfterVideo ();
        }
    }

    private Tile GetTile (RaycastHit2D rayHit)
    {
        Tile tile = null;
        try
        {
            tile = rayHit.collider.gameObject.GetComponent<Tile> ();
        }
        catch { }
        return tile;
    }
}