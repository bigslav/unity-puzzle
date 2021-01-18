using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapGenerator : MonoBehaviour
{
    [SerializeField] private Tilemap tileMap;
    [SerializeField] private TileManager tileManager;

    private int[,] _mapping;
    private int[,] _mappingInteractiveLayout =
    {
        { 3, 2, 3 },
        { 4, 2, 3 },
        { 2, 4, 4 },
        { 3, 3, 2 },
        { 4, 4, 2 }
    };
    private readonly int[,] _mappingBlockLayout =
    {
        { 0, 1, 0, 1, 0 },
        { 0, 0, 0, 0, 0 },
        { 0, 1, 0, 1, 0 },
        { 0, 0, 0, 0, 0 },
        { 0, 1, 0, 1, 0 }
    };

    public void GenerateTileMap()
    {
        _mapping = GenerateRandomMapping();
        for (int x = 0; x < 5; x++)
        {
            for (int y = 0; y < 5; y++)
            {
                switch (_mapping[x, y])
                {
                    case 0:
                        tileMap.SetTile(new Vector3Int(x, y, 0), tileManager.GetTile("empty"));
                        break;
                    case 1:
                        tileMap.SetTile(new Vector3Int(x, y, 0), tileManager.GetTile("blocked"));
                        break;
                    case 2:
                        tileMap.SetTile(new Vector3Int(x, y, 0), tileManager.GetTile("purple"));
                        break;
                    case 3:
                        tileMap.SetTile(new Vector3Int(x, y, 0), tileManager.GetTile("emerald"));
                        break;
                    case 4:
                        tileMap.SetTile(new Vector3Int(x, y, 0), tileManager.GetTile("red"));
                        break;
                }
            }
        }
    }

    private int[,] GenerateRandomMapping()
    {
        _mapping = _mappingBlockLayout;

        ShuffleMatrix(_mappingInteractiveLayout);

        for (int y = 0; y < 5; y++)
        {
            for (int x = 0; x < 3; x++)
            {
                switch (x)
                {
                    case 0:
                        _mapping[y, 0] = _mappingInteractiveLayout[y, x];
                        break;
                    case 1:
                        _mapping[y, 2] = _mappingInteractiveLayout[y, x];
                        break;
                    case 2:
                        _mapping[y, 4] = _mappingInteractiveLayout[y, x];
                        break;
                }
            }
        }

        return _mapping;
    }

    private void ShuffleMatrix(int[,] values)
    {
        int numberOfRows = values.GetUpperBound(0) + 1;
        int numberOfColumns = values.GetUpperBound(1) + 1;
        int numberOfCells = numberOfRows * numberOfColumns;

        System.Random rand = new System.Random();
        for (int i = 0; i < numberOfCells - 1; i++)
        {
            int j = rand.Next(i, numberOfCells);

            int rowI = i / numberOfColumns;
            int colI = i % numberOfColumns;
            int rowJ = j / numberOfColumns;
            int colJ = j % numberOfColumns;

            int temp = values[rowI, colI];
            values[rowI, colI] = values[rowJ, colJ];
            values[rowJ, colJ] = temp;
        }
    }
}