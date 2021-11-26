using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryBarController : MonoBehaviour
{
    private List<string> resourceList;
    private GameObject resourcePrefab;

    private Dictionary<string, TMP_Text> owns;
    private Dictionary<string, TMP_Text> cap;

    private List<float> barWidths;

    private void Awake()
    {
        barWidths = new List<float> { 0f, 120f, 200f, 290f };  // Index is how many resources we have;
        float xPos = 0f;

        resourceList = new List<string> { "Coin", "Wood", "Wool" };

        // TODO: Figure out auto-sizing for bar.
        //RectTransform barRect = transform.GetComponent<RectTransform>();
        //barRect.anchorMax = new Vector2 (barWidths[resourceList.Count], barRect.anchorMax.y);

        resourcePrefab = Resources.Load<GameObject>(HierarchyAddrs.GUIInventoryResource);
        owns = new Dictionary<string, TMP_Text>();
        cap = new Dictionary<string, TMP_Text>();

        foreach (string resourceName in resourceList)
        {
            GameObject res = Instantiate<GameObject>(resourcePrefab, transform);
            res.name = resourceName;
            res.transform.localPosition += new Vector3(xPos, 0, 0);
            res.GetComponentInChildren<Image>().sprite = ResourceInfo.resources[resourceName].sprite;
            owns.Add(resourceName, res.transform.Find("Value/Owns").GetComponent<TMP_Text>());
            cap.Add(resourceName, res.transform.Find("Value/Cap").GetComponent<TMP_Text>());

            xPos += 85;
        }
    }

    private void OnEnable()
    {
        Inventory.OnPlayerResourceChange += HandlePlayerResourceChange;
    }

    private void OnDisable()
    {
        Inventory.OnPlayerResourceChange -= HandlePlayerResourceChange;
    }

    private void HandlePlayerResourceChange(string name, int value)
    {
        owns[name].text = value.ToString().PadLeft(2, '0');
        cap[name].text = "/" + Inventory.GetItemCap(name).ToString().PadLeft(2, '0');
    }
}