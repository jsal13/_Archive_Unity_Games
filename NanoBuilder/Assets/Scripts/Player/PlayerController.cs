using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool canBuiildAtCurrentLocation;
    private GameObject placementIndicator;
    [SerializeField] private Vector2 _screenPosition;
    private Vector3 offset = new Vector3(8, 8, 0);
    public Vector2 ScreenPosition
    {
        get => _screenPosition;
        set
        {
            _screenPosition = value;
            worldPosition = Camera.main.ScreenToWorldPoint(_screenPosition);
            gridPosition = 16 * Vector3Int.RoundToInt((worldPosition - (Vector2)offset) / 16) + offset;
        }
    }
    private Vector2 worldPosition;
    [SerializeField] private Vector3 gridPosition;

    private void Update()
    {
        if (GameManager.Instance.CurrentSelectedBuilding != null)
        {
            if (placementIndicator == null)
            {
                placementIndicator = Instantiate(GameManager.Instance.placementIndicatorPrefab);
            }

            ScreenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            Debug.DrawRay(gridPosition, 8 * Vector3.up, Color.cyan);
            Debug.DrawRay(gridPosition, 8 * Vector3.right, Color.cyan);

            DrawPlacementIndicator(GameManager.Instance.CurrentSelectedBuilding);

            if (Input.GetMouseButtonDown(0))
            {
                PlaceCurrentBuilding();
            }
        }
        else
        {
            if (placementIndicator != null)
            {
                Destroy(placementIndicator);
            }
        }
    }

    private void DrawPlacementIndicator(string building)
    {
        placementIndicator.transform.position = gridPosition;
        if (placementIndicator.GetComponent<BoxCollider2D>().IsTouchingLayers(LayerMask.GetMask("Obstruction")))
        {
            placementIndicator.GetComponent<SpriteRenderer>().color = Color.red;
        }
        else
        {
            placementIndicator.GetComponent<SpriteRenderer>().color = Color.green;
        }
        //canBuiildAtCurrentLocation

    }

    private void PlaceCurrentBuilding()
    {
        Instantiate(GameManager.Instance.currentSelectedBuildingPrefab, gridPosition, Quaternion.identity);
    }


}
