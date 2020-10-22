using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent (typeof (Image), typeof (RectTransform))]
public class MainTile : MonoBehaviour, IDragHandler, IEndDragHandler
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
        Ray2D ray = new Ray2D (transform.position, transform.right);
        RaycastHit2D[] hits = Physics2D.RaycastAll (ray.origin, ray.direction, 10f);
        for (int i = 0; i < hits.Length; i++)
        {
            Tile tile = hits[i].collider.gameObject.GetComponent<Tile> ();
            if (tile)
            {
                GameHelper.Instance.SwapTwoTiles (this, tile);
                break;
            }
        }
        _transform.position = _startPosition;
    }
}