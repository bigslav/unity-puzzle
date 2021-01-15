using UnityEngine;
using UnityEngine.Tilemaps;

public class InputHandler : MonoBehaviour
{
    [SerializeField]
    private TileRepositioner tileRepositioner;
    [SerializeField]
    private Tilemap tileMap;

    private void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        tileRepositioner.SetMousePosition(mousePos);

        if (Input.GetMouseButtonDown(0))
        {
            tileRepositioner.OnMouseDown();
        }
    }
}
