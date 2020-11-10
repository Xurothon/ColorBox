using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent (typeof (Image), typeof (RectTransform))]
public class MainTile : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerDownHandler
{
    public Image image;
    private RectTransform _transform;
    private Vector3 _startPosition;
    private Canvas _canvas;

    private void Awake ()
    {
        _transform = GetComponent<RectTransform> ();
        _canvas = transform.parent.GetComponent<Canvas> ();
        _startPosition = _transform.position;
    }

    public void OnDrag (PointerEventData eventData)
    {
        _transform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
    }

    public void OnEndDrag (PointerEventData eventData)
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
            GameHelper.Instance.SwapTwoTiles (this, hitLeft.collider.gameObject.GetComponent<Tile> ());
        }
        _transform.position = _startPosition;
    }

    public void OnPointerDown (PointerEventData eventData)
    {
        SoundsHelper.Instance.PlayTakeTileClip ();
    }
}