using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    [SerializeField] private Tile[] tiles;

    public TileBase GetTile(string name)
    {
        TileBase targetTile = null;

        foreach (Tile tile in tiles)
        {
            if (tile.name == name)
            {
                targetTile = tile.tileBase;
            }
        }

        return targetTile;
    }

    [Serializable]
    public struct Tile
    {
        public string name;
        public TileBase tileBase;
    }
}
