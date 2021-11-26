using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkshopController : MonoBehaviour
{
    private void Start()
    {
        BuildingManager.Instance.obstructionTilemap.SetTile(BuildingManager.Instance.worldGrid.WorldToCell(transform.localPosition), BuildingManager.Instance.obstructionTile);
    }
}
