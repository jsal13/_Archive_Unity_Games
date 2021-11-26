using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public void SetCurrentSelectedBuilding(string building)
    {
        if (GameManager.Instance.CurrentSelectedBuilding == building)
        {
            GameManager.Instance.CurrentSelectedBuilding = null;
        }

        GameManager.Instance.CurrentSelectedBuilding = building;
    }

    public string GetCurrentSelectedBuilding()
    {
        return GameManager.Instance.CurrentSelectedBuilding;
    }
}
