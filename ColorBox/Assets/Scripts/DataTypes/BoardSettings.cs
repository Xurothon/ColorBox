using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BoardSettings
{
    public int xSize, ySize;
    public Tile tile;
    public List<Sprite> tileSprites;
    public bool useBlocks;
    public Sprite blockSprite;
}