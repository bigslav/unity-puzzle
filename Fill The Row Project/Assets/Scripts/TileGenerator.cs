using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class TileGenerator : MonoBehaviour
{
    public TileBase empty;
    public TileBase blocked;
    public TileBase yellow;
    public TileBase orange;
    public TileBase red;
    public TileBase pink;

    public Tilemap tileMap;
    public Grid grid;
    public Vector3Int selected_pos;
    public Vector3Int validation;
    public TileBase selected_tile;

    public Text congrats_text;
    public int[,] tiles;

    bool is_tile_selected;
    private void Start()
    {
        is_tile_selected = false;
        tiles = new int[5, 5] { { 3, 1, 2, 1, 3 }, { 4, 0, 2, 0, 3 }, { 2, 1, 4, 1, 4 }, { 3, 0, 3, 0, 2 }, { 4, 1, 4, 1, 2 } };
        placeTiles();

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            
            if (is_tile_selected == false) {
                
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3Int gridPos = tileMap.WorldToCell(mousePos);

                if (tileMap.HasTile(gridPos) && (tileMap.GetTile(gridPos) == empty || tileMap.GetTile(gridPos) == blocked || (gridPos[1] == 2)))
                {
                    
                }
                else if(tileMap.HasTile(gridPos)) {
                    selected_tile = tileMap.GetTile(gridPos);
                    tileMap.SetTile(gridPos, pink);
                    is_tile_selected = true;
                    selected_pos = gridPos;   
                }

            } else if (is_tile_selected == true) {

                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3Int gridPos = tileMap.WorldToCell(mousePos);
                
                if (tileMap.HasTile(gridPos) && (gridPos != selected_pos) && (gridPos[1]!=2))
                {

                    if (((tileMap.GetTile(gridPos) != empty) && (tileMap.GetTile(gridPos) != blocked)))
                    {

                        tileMap.SetTile(selected_pos, selected_tile);
                        selected_tile = tileMap.GetTile(gridPos);
                        tileMap.SetTile(gridPos, pink);
                        is_tile_selected = true;
                        selected_pos = gridPos;

                    }
                    else if ((tileMap.GetTile(gridPos) == empty) && ((gridPos[0] != selected_pos[0]) && (gridPos[1] != selected_pos[1])) || ((Mathf.Abs(gridPos[0] - selected_pos[0]) > 1) || (Mathf.Abs(gridPos[1] - selected_pos[1]) > 1)))
                    {


                    }
                    else if ((tileMap.GetTile(gridPos) == empty) && !((gridPos[0] != selected_pos[0]) && (gridPos[1] != selected_pos[1])) || ((Mathf.Abs(gridPos[0] - selected_pos[0]) > 1) || (Mathf.Abs(gridPos[1] - selected_pos[1]) > 1)))
                    {
                        TileBase tileHolder = tileMap.GetTile(gridPos);

                        tileMap.SetTile(gridPos, selected_tile);
                        tileMap.SetTile(selected_pos, tileHolder);
                        is_tile_selected = false;
                    }

                }
                else if (tileMap.HasTile(gridPos) && (gridPos == selected_pos))
                {
                    tileMap.SetTile(gridPos, selected_tile);
                    is_tile_selected = false;
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
                
                switch (tiles[y, x]) {
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

        int[,] coretiles = new int[5, 3] { { 3, 2, 3 }, { 4, 2, 3 }, { 2, 4, 4 }, { 3, 3, 2 }, { 4, 4, 2 } };
        Randomizer.Randomize(coretiles);
        tiles = new int[5, 5] { { 0, 1, 0, 1, 0 }, { 0, 0, 0, 0, 0 }, { 0, 1, 0, 1, 0 }, { 0, 0, 0, 0, 0 }, { 0, 1, 0, 1, 0 } };

        for (int x = 0; x < 5; x++) {
            for (int y = 0; y < 3; y++) {
                switch (y) {
                    case 0:
                        tiles[x, 0] = coretiles[x, y];
                        break;
                    case 1:
                        tiles[x, 2] = coretiles[x, y];
                        break;
                    case 2:
                        tiles[x, 4] = coretiles[x, y];
                        break;
                }
                    
            }
        }
        congrats_text.text = "";

    }

    void CheckForVictory() {

        int yellow_count = 0;
        int orange_count = 0;
        int red_count = 0;

        for (int y = -4; y <= 0; y++) {

            

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
            congrats_text.text = "YOU WON! WELL DONE!";
        }
    }

    
}
