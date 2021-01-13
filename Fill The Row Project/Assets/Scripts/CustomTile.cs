using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CustomTile : Tile
{
    [SerializeField]
    private Sprite[] _sprites;

    public bool IsEmpty { get { return isEmpty; } }
    public bool IsBlocked { get { return isBlocked; } }

    private bool isEmpty;
    private bool isBlocked;

    public CustomTile()
    { 
    
    }
}
