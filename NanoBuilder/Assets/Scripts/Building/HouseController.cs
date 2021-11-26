using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HouseController : MonoBehaviour
{
    private void Start()
    {
        BuildingManager.Instance.obstructionTilemap.SetTile(BuildingManager.Instance.worldGrid.WorldToCell(transform.localPosition), BuildingManager.Instance.obstructionTile);
    }
}
