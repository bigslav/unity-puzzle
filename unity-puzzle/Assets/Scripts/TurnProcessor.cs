using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class TurnProcessor : MonoBehaviour
{
    [SerializeField] private Tilemap tileMap;
    [SerializeField] private TileMapGenerator tileMapGenerator;
    [SerializeField] private string firstColumnTileColor;
    [SerializeField] private string secondColumnTileColor;
    [SerializeField] private string thirdColumnTileColor;
    [SerializeField] private Text turnCounterText;
    [SerializeField] private Text victoryText;

    private int _firstColumnCounter;
    private int _secondColumnCounter;
    private int _thirdColumnCounter;
    private int _turnCounterValue;

    private void Awake()
    {
        _firstColumnCounter = 0;
        _secondColumnCounter = 0;
        _thirdColumnCounter = 0;

        RestartGameSession();
    }

    public void CheckForVictory()
    {
        for (int y = 0; y < 5; y++)
        {
            if (tileMap.GetTile(new Vector3Int(y, 0, 0)).name == firstColumnTileColor)
            {
                _firstColumnCounter++;
            }
            if (tileMap.GetTile(new Vector3Int(y, 2, 0)).name == secondColumnTileColor)
            {
                _secondColumnCounter++;
            }
            if (tileMap.GetTile(new Vector3Int(y, 4, 0)).name == thirdColumnTileColor)
            {
                _thirdColumnCounter++;
            }
        }

        if (_firstColumnCounter + _secondColumnCounter + _thirdColumnCounter == 15)
        {
            victoryText.text = "Congrats! You won!";
        }
        else
        {
            _firstColumnCounter = 0;
            _secondColumnCounter = 0;
            _thirdColumnCounter = 0;
        }
    }

    public void CountTurn()
    {
        _turnCounterValue++;
        turnCounterText.text = _turnCounterValue.ToString();
    }

    public void RestartGameSession()
    {
        victoryText.text = "";
        tileMapGenerator.GenerateTileMap();

        _turnCounterValue = 0;
        turnCounterText.text = _turnCounterValue.ToString();
    }
}
