using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class VictoryChecker : MonoBehaviour
{
    public Tilemap tileMap;

    public string firstColumnTileColor;
    public string secondColumnTileColor;
    public string thirdColumnTileColor;

    private int firstColumnCounter;
    private int secondColumnCounter;
    private int thirdColumnCounter;

    private void Awake()
    {
        firstColumnCounter = 0;
        secondColumnCounter = 0;
        thirdColumnCounter = 0;
    }

    public void CheckForVictory() 
    {
        for (int y = 0; y < 5; y++)
        {
            if (tileMap.GetTile(new Vector3Int(y, 0, 0)).name == firstColumnTileColor)
            {
                firstColumnCounter++;
            }
            if (tileMap.GetTile(new Vector3Int(y, 2, 0)).name == secondColumnTileColor)
            {
                secondColumnCounter++;
            }
            if (tileMap.GetTile(new Vector3Int(y, 4, 0)).name == thirdColumnTileColor)
            {
                thirdColumnCounter++;
            }
        }

        if (firstColumnCounter + secondColumnCounter + thirdColumnCounter == 15)
        {
            Debug.Log("Victory");
        }
        else 
        {
            firstColumnCounter = 0;
            secondColumnCounter = 0;
            thirdColumnCounter = 0;
        }
    }
}
