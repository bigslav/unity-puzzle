using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class MonsterClass : MonoBehaviour
{
    public TileBase empty;
    public TileBase blocked;
    public TileBase yellow;
    public TileBase orange;
    public TileBase red;
    public TileBase pink;
    public Tilemap tileMap;
    public Grid grid;
    public Text congratsText;
    private Vector3Int selectedTileBasePos;
    private TileBase selectedTileBase;
    private int[,] tiles;
    private bool isTileSelected;

    private void Start()
    {
        isTileSelected = false;
        tiles = new int[5, 5] { { 3, 1, 2, 1, 3 }, { 4, 0, 2, 0, 3 }, { 2, 1, 4, 1, 4 }, { 3, 0, 3, 0, 2 }, { 4, 1, 4, 1, 2 } };
        placeTiles();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            if (isTileSelected == false) 
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3Int gridPos = tileMap.WorldToCell(mousePos);

                if (tileMap.HasTile(gridPos) && (tileMap.GetTile(gridPos) == empty || tileMap.GetTile(gridPos) == blocked || (gridPos[1] == 2)))
                {
                    
                }
                else if (tileMap.HasTile(gridPos)) 
                {
                    selectedTileBase = tileMap.GetTile(gridPos);
                    tileMap.SetTile(gridPos, pink);
                    isTileSelected = true;
                    selectedTileBasePos = gridPos;   
                }
            } 
            else if (isTileSelected == true) 
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3Int gridPos = tileMap.WorldToCell(mousePos);
                
                if (tileMap.HasTile(gridPos) && (gridPos != selectedTileBasePos) && (gridPos[1]!=2))
                {
                    if (((tileMap.GetTile(gridPos) != empty) && (tileMap.GetTile(gridPos) != blocked)))
                    {
                        tileMap.SetTile(selectedTileBasePos, selectedTileBase);
                        selectedTileBase = tileMap.GetTile(gridPos);
                        tileMap.SetTile(gridPos, pink);
                        isTileSelected = true;
                        selectedTileBasePos = gridPos;
                    }
                    else if ((tileMap.GetTile(gridPos) == empty) && ((gridPos[0] != selectedTileBasePos[0]) && (gridPos[1] != selectedTileBasePos[1])) || ((Mathf.Abs(gridPos[0] - selectedTileBasePos[0]) > 1) || (Mathf.Abs(gridPos[1] - selectedTileBasePos[1]) > 1)))
                    {

                    }
                    else if ((tileMap.GetTile(gridPos) == empty) && !((gridPos[0] != selectedTileBasePos[0]) && (gridPos[1] != selectedTileBasePos[1])) || ((Mathf.Abs(gridPos[0] - selectedTileBasePos[0]) > 1) || (Mathf.Abs(gridPos[1] - selectedTileBasePos[1]) > 1)))
                    {
                        TileBase tileHolder = tileMap.GetTile(gridPos);
                        tileMap.SetTile(gridPos, selectedTileBase);
                        tileMap.SetTile(selectedTileBasePos, tileHolder);
                        isTileSelected = false;
                    }
                }
                else if (tileMap.HasTile(gridPos) && (gridPos == selectedTileBasePos))
                {
                    tileMap.SetTile(gridPos, selectedTileBase);
                    isTileSelected = false;
                }
            }
            CheckForVictory();
        }
    }

    public void placeTiles() {
        for (int y = 0; y < 5; y++)    
        {
            for (int x = 4; x >= 0; x--)
            {
                switch (tiles[y, x]) 
                {
                    case 0:
                        tileMap.SetTile(new Vector3Int(x, -y, 0), empty);
                        break;
                    case 1:
                        tileMap.SetTile(new Vector3Int(x, -y, 0), blocked);
                        break;
                    case 2:
                        tileMap.SetTile(new Vector3Int(x, -y, 0), yellow);
                        break;
                    case 3:
                        tileMap.SetTile(new Vector3Int(x, -y, 0), orange);
                        break;
                    case 4:
                        tileMap.SetTile(new Vector3Int(x, -y, 0), red);
                        break;
                }
            }
        }

        int[,] coreTiles = new int[5, 3] { { 3, 2, 3 }, { 4, 2, 3 }, { 2, 4, 4 }, { 3, 3, 2 }, { 4, 4, 2 } };
        Randomizer.Randomize(coreTiles);
        tiles = new int[5, 5] { { 0, 1, 0, 1, 0 }, { 0, 0, 0, 0, 0 }, { 0, 1, 0, 1, 0 }, { 0, 0, 0, 0, 0 }, { 0, 1, 0, 1, 0 } };

        for (int x = 0; x < 5; x++) 
        {
            for (int y = 0; y < 3; y++) 
            {
                switch (y)
                {
                    case 0:
                        tiles[x, 0] = coreTiles[x, y];
                        break;
                    case 1:
                        tiles[x, 2] = coreTiles[x, y];
                        break;
                    case 2:
                        tiles[x, 4] = coreTiles[x, y];
                        break;
                } 
            }
        }
        congratsText.text = "";
    }

    private void CheckForVictory() 
    {
        int yellow_count = 0;
        int orange_count = 0;
        int red_count = 0;

        for (int y = -4; y <= 0; y++) 
        {
            if (tileMap.GetTile(new Vector3Int(0, y, 0)) == yellow)
            {
                yellow_count++;
            }
            if (tileMap.GetTile(new Vector3Int(2, y, 0)) == orange)
            {
                orange_count++;
            }
            if (tileMap.GetTile(new Vector3Int(4, y, 0)) == red)
            {
                red_count++;
            }
        }

        if (yellow_count+orange_count+red_count == 15)
        {
            congratsText.text = "YOU WON! WELL DONE!";
        }
    }
}
