
using UnityEngine;

public class BackgroundCreator : MonoBehaviour
{
    [SerializeField] private Tile _background;
    [SerializeField] private Sprite _outline;
    private int _xSize, _ySize;

    public void SetValues(int xSize, int ySize)
    {
        _xSize = xSize + 2;
        _ySize = ySize + 2;
        CreateBoard();
    }

    private void CreateBoard()
    {
        float xPos = transform.position.x;
        float yPos = transform.position.y;
        Vector2 tileSize = _background.GetComponent<SpriteRenderer>().bounds.size;
        for (int y = 0; y < _ySize; y++)
        {
            for (int x = 0; x < _xSize; x++)
            {
                Tile newTile = Instantiate(_background, transform.position, Quaternion.identity);
                newTile.transform.position = new Vector3(xPos + (tileSize.x * x), yPos + (tileSize.y * y), 0);
                newTile.transform.parent = transform;
                if (x == 0 || y == 0 || x == (_xSize - 1) || y == (_ySize - 1))
                {
                    newTile.spriteRenderer.sprite = _outline;
                }
            }
        }
    }
}
