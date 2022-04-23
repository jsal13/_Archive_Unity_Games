using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public enum NodeState { CanVisit, CannotVisit };

public class MapNode
{
    /* 
        Single node object on the map. 
        
        nodeState: Gives the state of the node, either visited or not visited.
        xPos: Horizontal position on the map, relative to the container.
        yPos: Vertical position on the map, relative to the container.
    */
    public NodeState _state;
    public NodeState State
    {
        get => _state;
        set
        {
            // Remove old Classes.
            nodeBtn.RemoveFromClassList("map-node-can-visit");
            nodeBtn.RemoveFromClassList("map-node-cannot-visit");

            if (value == NodeState.CanVisit)
            {
                nodeBtn.AddToClassList("map-node-can-visit");
            }
            else
            {
                nodeBtn.AddToClassList("map-node-cannot-visit");
            }
        }
    }

    public float xPos;
    public float yPos;
    public Button nodeBtn;

    public MapNode(float xPos, float yPos)
    {
        this.xPos = xPos;
        this.yPos = yPos;

        this.nodeBtn = new Button();
        this.nodeBtn.style.position = Position.Absolute;
        this.nodeBtn.AddToClassList("map-node");

        // TODO: CHANGE THIS!
        this.nodeBtn.name = $"{xPos}-{yPos}-{State.ToString()}-{UnityEngine.Random.Range(1, 10000)}";
        this.nodeBtn.style.left = this.xPos;
        this.nodeBtn.style.top = this.yPos;

        this.nodeBtn.clicked += HandleClick;

        void HandleClick()
        {
            Debug.Log($"Hi, I'm {this.nodeBtn.name}.");
        }
    }
}

public class MapNodes
{
    // Currently randomizes mapnodes.

    public List<MapNode> mapNodes;
    public float horizPadding = 100;

    public MapNodes()
    {
        this.mapNodes = new List<MapNode>();
    }

    public List<MapNode> CreateRandomMap()
    {
        for (int idx = 0; idx <= 4; idx += 1)
        {
            int r = UnityEngine.Random.Range(1, 3);
            for (int jdx = 0; jdx <= r; jdx += 1)
            {
                int randY = UnityEngine.Random.Range(20, 300);
                MapNode currentNode = new MapNode((1 + idx) * horizPadding, randY);

                if (idx == 2) // DEBUG!
                {
                    currentNode.State = NodeState.CanVisit;
                }
                else
                {
                    currentNode.State = NodeState.CannotVisit;
                }

                mapNodes.Add(currentNode);
            }
        }
        return mapNodes;
    }
}


public class NodeController : MonoBehaviour
{
    private VisualElement root;
    private VisualElement mapContainer;
    [SerializeField] private List<MapNode> mapNodes;

    public static Action OnRefreshMapNodes;

    private void Awake()
    {
        root = transform.GetComponent<UIDocument>().rootVisualElement;
        mapContainer = root.Q<VisualElement>("container-map");
        RefreshNodes();
    }

    private void RemoveAllNodes()
    {
        foreach (VisualElement child in mapContainer.Children())
        {
            mapContainer.Remove(child);
        }
    }

    private void RefreshNodes()
    {
        mapNodes = new MapNodes().CreateRandomMap();
        foreach (MapNode mapNode in mapNodes)
        {
            mapContainer.Add(mapNode.nodeBtn);
        }
    }

    private void OnEnable()
    {
        OnRefreshMapNodes += HandleRefreshMapNodes;
    }

    private void OnDisable()
    {
        OnRefreshMapNodes -= HandleRefreshMapNodes;
    }

    public void HandleRefreshMapNodes()
    {
        RemoveAllNodes();
        RefreshNodes();
    }

}