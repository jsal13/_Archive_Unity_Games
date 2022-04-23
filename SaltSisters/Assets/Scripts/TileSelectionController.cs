using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileSelectionController : MonoBehaviour
{
    [SerializeField] private Grid grid;
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private Tilemap obstructionTileMap;
    [SerializeField] private Tilemap gameTileMap;
    [SerializeField] private Tile obstructionTile;

    //TODO: Make this more general.
    [SerializeField] private Tile settlementTile;

    [SerializeField] private Vector3Int previousMousePos = new Vector3Int();
    [SerializeField] private Vector3Int currentMousePos = new Vector3Int();

    private void Update()
    {
        currentMousePos = GetMousePosition();
        if (!currentMousePos.Equals(previousMousePos))
        {
            tilemap.SetTile(previousMousePos, null);
            tilemap.SetTile(currentMousePos, obstructionTile);
            previousMousePos = currentMousePos;
        }

        if (GameManager.mouse.leftButton.isPressed)
        {
            if (obstructionTileMap.GetTile<Tile>(currentMousePos) != null)
            {
                Debug.Log("Obstructed!");
            }
            else
            {
                gameTileMap.SetTile(currentMousePos, settlementTile);
                obstructionTileMap.SetTile(currentMousePos, obstructionTile);
            }
        }
    }

    private Vector3Int GetMousePosition()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(GameManager.mouse.position.ReadValue());
        return grid.WorldToCell(mouseWorldPos);
    }
}
