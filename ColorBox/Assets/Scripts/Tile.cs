using UnityEngine;

public class Tile : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite playerSprite;
    public Sprite blockSprite;
    public Sprite enemySprite;
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

    public bool isPlayer
    {
        get
        {
            return spriteRenderer.sprite == playerSprite?true : false;
        }
    }

    public bool isEnemy
    {
        get
        {
            return spriteRenderer.sprite == enemySprite?true : false;
        }
    }
}