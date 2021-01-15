using UnityEngine;
using UnityEngine.Tilemaps;

public class TileRepositioner : MonoBehaviour
{
    [SerializeField]
    private Tilemap tileMap;
    [SerializeField]
    private TileManager tileManager;
    [SerializeField]
    private VictoryChecker victoryChecker;

    private Vector2 _mousePosition;
    private bool _isTileSelected;
    private TileBase _selectedTile;
    private Vector3Int _selectedTilePos;

    public void OnMouseDown()
    {
        Vector3Int gridPos = tileMap.WorldToCell(_mousePosition);

        if (tileMap.HasTile(gridPos)) 
        {
            if (_isTileSelected == false)
            {
                if (CheckIfTileCanBeSelected(gridPos))
                {
                    SelectTile(gridPos);
                }
            }
            else if (_isTileSelected == true)
            {
                if (gridPos != _selectedTilePos)
                {
                    if (CheckIfTilesCanBeSwapped(_selectedTilePos, gridPos))
                    {
                        SwapTiles(_selectedTilePos, gridPos);
                        victoryChecker.CheckForVictory();
                    }
                    else if (CheckIfTileIsInteractive(gridPos))
                    {
                        UnselectTile(_selectedTilePos);
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

    public void SetMousePosition(Vector2 mousePosition)
    {
        _mousePosition = mousePosition;
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
        tileMap.SetTile(gridPos, _selectedTile);
        _isTileSelected = false;
    }

    private void SelectTile(Vector3Int gridPos)
    {
        _selectedTile = tileMap.GetTile(gridPos);
        tileMap.SetTile(gridPos, tileManager.GetTileBase("pink"));
        _selectedTilePos = gridPos;
        _isTileSelected = true;
    }

    private void SwapTiles(Vector3Int gridPosSelected, Vector3Int gridPosTarget)
    {
        TileBase tileHolder = tileMap.GetTile(gridPosTarget);
        tileMap.SetTile(gridPosTarget, _selectedTile);
        tileMap.SetTile(gridPosSelected, tileHolder);
        _isTileSelected = false;
    }
}
