using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager: MonoBehaviour
{
    [SerializeField]
    private Tile[] tiles;

    public TileBase GetTileBase(string name)
    {
        TileBase targetTileBase = null;

        foreach (Tile tile in tiles) 
        {
            if (tile.name == name)
            {
                targetTileBase = tile.tileBase;
            }
        }

        return targetTileBase;
    }

    [Serializable]
    public struct Tile 
    {
        public string name;
        public TileBase tileBase;
    }
}
