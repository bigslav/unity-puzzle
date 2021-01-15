using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileRepositioner : MonoBehaviour
{
    private Vector2 _mousePosition;
    public Tilemap tileMap;
    public TileManager tileManager;
    public VictoryChecker victoryChecker;
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

        if (tileMap.HasTile(gridPos)) 
        {
            if (isTileSelected == false)
            {
                if (CheckIfTileCanBeSelected(gridPos))
                {
                    SelectTile(gridPos);
                }
            }
            else if (isTileSelected == true)
            {
                if (gridPos != selectedTileBasePos)
                {
                    if (CheckIfTilesCanBeSwapped(selectedTileBasePos, gridPos))
                    {
                        SwapTiles(selectedTileBasePos, gridPos);
                        victoryChecker.CheckForVictory();
                    }
                    else if (CheckIfTileIsInteractive(gridPos))
                    {
                        UnselectTile(selectedTileBasePos);
                        SelectTile(gridPos);
                    }
                }
                else
                {
                    UnselectTile(gridPos);
                }
            }
        }
        
    }

    private bool CheckIfTileIsInteractive(Vector3Int gridPos)
    {
        if (tileMap.GetTile(gridPos).name != "empty" && tileMap.GetTile(gridPos).name != "blocked")
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool CheckIfTilesCanBeSwapped(Vector3Int selectedGridPos, Vector3Int targetGridPos)
    {
        if (tileMap.GetTile(targetGridPos).name == "empty")
        {
            if ((selectedGridPos[0] == targetGridPos[0]) || (selectedGridPos[1] == targetGridPos[1]))
            {
                if ((Mathf.Abs(targetGridPos[0] - selectedGridPos[0]) == 1) || (Mathf.Abs(targetGridPos[1] - selectedGridPos[1]) == 1))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        else 
        {
            return false;
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
