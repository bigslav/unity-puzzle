using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileRepositioner : MonoBehaviour
{
    private Vector2 _mousePosition;
    public Tilemap tileMap;
    public TileManager tileManager;
    private bool isTileSelected;
    private TileBase selectedTileBase;
    private Vector3Int selectedTileBasePos;

    public void SetMousePosition(Vector2 mousePosition) 
    {
        _mousePosition = mousePosition;
    }

    public void OnMouseDown()
    {
        Vector3Int gridPos = tileMap.WorldToCell(_mousePosition);
        if (isTileSelected == false)
        {
            if (CheckIfTileCanBeSelected(gridPos))
            {
                SelectTile(gridPos);
            }
        }
        else if (isTileSelected == true)
        {
            SwapTiles(selectedTileBasePos, gridPos);
            if (tileMap.HasTile(gridPos) && (gridPos != selectedTileBasePos) && (gridPos[1] != 2))
            {
                if (((tileMap.GetTile(gridPos).name == "empty") && (tileMap.GetTile(gridPos).name != "blocked")))
                {
                    UnselectTile(selectedTileBasePos);
                    SelectTile(gridPos);
                }
                else if ((tileMap.GetTile(gridPos).name == "empty") && ((gridPos[0] != selectedTileBasePos[0]) && (gridPos[1] != selectedTileBasePos[1])) || ((Mathf.Abs(gridPos[0] - selectedTileBasePos[0]) > 1) || (Mathf.Abs(gridPos[1] - selectedTileBasePos[1]) > 1)))
                {

                }
                else if ((tileMap.GetTile(gridPos).name == "empty") && !((gridPos[0] != selectedTileBasePos[0]) && (gridPos[1] != selectedTileBasePos[1])) || ((Mathf.Abs(gridPos[0] - selectedTileBasePos[0]) > 1) || (Mathf.Abs(gridPos[1] - selectedTileBasePos[1]) > 1)))
                {
                    SwapTiles(selectedTileBasePos, gridPos);

                }
            }
            else if (tileMap.HasTile(gridPos) && (gridPos == selectedTileBasePos))
            {
                tileMap.SetTile(gridPos, selectedTileBase);
                isTileSelected = false;
            }
        }
    }

    private bool CheckIfTileCanBeSelected(Vector3Int gridPos) 
    {
        if (tileMap.HasTile(gridPos) && !(tileMap.GetTile(gridPos).name == "empty" || tileMap.GetTile(gridPos).name == "blocked"))
        {
            return true;
        }
        else 
        {
            return false;
        }
    }

    private void UnselectTile(Vector3Int gridPos)
    {
        tileMap.SetTile(gridPos, selectedTileBase);
        isTileSelected = false;
    }

    private void SelectTile(Vector3Int gridPos)
    {
        selectedTileBase = tileMap.GetTile(gridPos);
        tileMap.SetTile(gridPos, tileManager.GetTileBase("pink"));
        selectedTileBasePos = gridPos;
        isTileSelected = true;
    }

    private void SwapTiles(Vector3Int gridPosSelected, Vector3Int gridPosTarget)
    {
        TileBase tileHolder = tileMap.GetTile(gridPosTarget);
        tileMap.SetTile(gridPosTarget, selectedTileBase);
        tileMap.SetTile(gridPosSelected, tileHolder);
        isTileSelected = false;
    }
}
