using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapGenerator : MonoBehaviour
{
    public Tilemap tileMap;
    public TileManager tileManager;

    private int[,] mapping;

    private int[,] mappingInteractiveLayout =
        
    {
        { 3, 2, 3 },
        { 4, 2, 3 },
        { 2, 4, 4 },
        { 3, 3, 2 },
        { 4, 4, 2 }
    };

    private readonly int[,] mappingBlockLayout =

    {
        { 0, 1, 0, 1, 0 },
        { 0, 0, 0, 0, 0 },
        { 0, 1, 0, 1, 0 },
        { 0, 0, 0, 0, 0 },
        { 0, 1, 0, 1, 0 }
    };

    private void Start()
    {
        GenerateTileMap(false);
    }

    public void GenerateTileMap(bool newMap = true)
    {
        mapping = GenerateMapping(newMap);
        for (int x = 0; x < 5; x++) 
        {
            for (int y = 0; y < 5; y++) 
            {
                switch (mapping[x, y]) 
                {
                    case 0:
                        tileMap.SetTile(new Vector3Int(x, y, 0), tileManager.GetTileBase("empty"));
                        break;
                    case 1:
                        tileMap.SetTile(new Vector3Int(x, y, 0), tileManager.GetTileBase("blocked"));
                        break;
                    case 2:
                        tileMap.SetTile(new Vector3Int(x, y, 0), tileManager.GetTileBase("yellow"));
                        break;
                    case 3:
                        tileMap.SetTile(new Vector3Int(x, y, 0), tileManager.GetTileBase("orange"));
                        break;
                    case 4:
                        tileMap.SetTile(new Vector3Int(x, y, 0), tileManager.GetTileBase("red"));
                        break;
                }
            }
        }
    }

    private int[,] GenerateMapping(bool newMapping)
    {
        mapping = mappingBlockLayout;

        if (newMapping)
        {
            ShuffleMatrix(mappingInteractiveLayout);
        }

        for (int y = 0; y < 5; y++)
        {
            for (int x = 0; x < 3; x++)
            {
                switch (x)
                {
                    case 0:
                        mapping[y, 0] = mappingInteractiveLayout[y, x];
                        break;
                    case 1:
                        mapping[y, 2] = mappingInteractiveLayout[y, x];
                        break;
                    case 2:
                        mapping[y, 4] = mappingInteractiveLayout[y, x];
                        break;
                }
            }
        }

        return mapping;
    }

    private void ShuffleMatrix(int[,] values)
    {
        int num_rows = values.GetUpperBound(0) + 1;
        int num_cols = values.GetUpperBound(1) + 1;
        int num_cells = num_rows * num_cols;

        System.Random rand = new System.Random();
        for (int i = 0; i < num_cells - 1; i++)
        {
            int j = rand.Next(i, num_cells);

            int row_i = i / num_cols;
            int col_i = i % num_cols;
            int row_j = j / num_cols;
            int col_j = j % num_cols;

            int temp = values[row_i, col_i];
            values[row_i, col_i] = values[row_j, col_j];
            values[row_j, col_j] = temp;
        }
    }
}