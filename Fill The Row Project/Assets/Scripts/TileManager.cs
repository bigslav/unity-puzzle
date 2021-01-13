using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager: MonoBehaviour
{
    public Tile[] tiles;

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
