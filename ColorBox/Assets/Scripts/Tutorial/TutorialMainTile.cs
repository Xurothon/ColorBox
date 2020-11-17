using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent (typeof (Image), typeof (RectTransform))]
public class TutorialMainTile : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerDownHandler
{
    public Image image;
    private RectTransform _transform;
    private Vector3 _startPosition;
    private Canvas _canvas;
    private Tile _cashTile;

    private void Awake ()
    {
        _transform = GetComponent<RectTransform> ();
        _canvas = transform.parent.GetComponent<Canvas> ();
        _startPosition = _transform.position;
    }

    public void OnDrag (PointerEventData eventData)
    {
        TutorialGameHelper.Instance.FinishTutorial1 ();
        _transform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
        Vector2 curMousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
        RaycastHit2D hitRight = Physics2D.Raycast (curMousePos, Vector2.right);
        Tile tile = GetTile (hitRight);
        if (tile != null)
        {
            RaycastHit2D hitLeft = Physics2D.Raycast (hitRight.collider.transform.position, Vector2.left);
            Tile leftTile = GetTile (hitLeft);
            if (leftTile != null)
            {
                if (_cashTile != null)
                {
                    MakeNormalTile (_cashTile);
                    MakeBigTile (leftTile);
                }
                else
                    MakeBigTile (leftTile);
            }
            else
            {
                if (_cashTile != null)
                    MakeNormalTile (_cashTile);
            }
        }
        else
        {
            if (_cashTile != null)
                MakeNormalTile (_cashTile);
        }
    }

    public void OnEndDrag (PointerEventData eventData)
    {
        MakeNormalTile (_cashTile);
        Vector2 curMousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
        RaycastHit2D hitRight = Physics2D.Raycast (curMousePos, Vector2.right);
        Tile tile = GetTile (hitRight);
        if (tile != null)
        {
            RaycastHit2D hitLeft = Physics2D.Raycast (hitRight.collider.transform.position, Vector2.left);
            Tile leftTile = GetTile (hitLeft);
            if (leftTile != null)
                TutorialGameHelper.Instance.SwapTwoTiles (this, hitLeft.collider.gameObject.GetComponent<Tile> ());
        }
        _transform.position = _startPosition;
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

    public void OnPointerDown (PointerEventData eventData)
    {
        SoundsHelper.Instance.PlayTakeTileClip ();
    }

    private void MakeBigTile (Tile tile)
    {
        tile.transform.localScale = new Vector3 (2f, 2f, 2f);
        tile.spriteRenderer.sortingOrder = 1;
        _cashTile = tile;
    }

    private void MakeNormalTile (Tile tile)
    {
        tile.transform.localScale = new Vector3 (1f, 1f, 1f);
        tile.spriteRenderer.sortingOrder = 0;
    }
}