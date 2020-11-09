using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public bool isBlock;
    public int xPosition;
    public int yPosition;
    public bool isEmpty
    {
        get
        {
            return spriteRenderer.sprite == null?true : false;
        }
    }
}