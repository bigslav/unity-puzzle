using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public TileRepositioner tileRepositioner;
    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        tileRepositioner.SetMousePosition(mousePos);

        if (Input.GetMouseButtonDown(0))
        {
            tileRepositioner.OnMouseDown();
        }
    }
}
