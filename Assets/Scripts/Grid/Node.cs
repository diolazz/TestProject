using UnityEngine;

/// <summary>
/// Main node class. Contains all info about node in grid
/// </summary>
public class Node
{
    private GameObject nodeObject;
    private Vector3 positionWorld;
    private int gridX;
    private int gridZ;
    private Color nodeColor;
    private ResourceObject objectOnNode;

    private bool isNodeHaveObject;

    public Material NodeMaterial { get; set; }
    public GameObject NodeObject {get { return nodeObject; }}

    public Vector3 PositionWorld { get { return positionWorld; } }

    public ResourceObject ObjectOnNode
    {
        get { return objectOnNode; }
        set { objectOnNode = value; }
    }

    public int GridX { get { return gridX; } }

    public int GridZ { get { return gridZ; } }

    public Color NodeColor
    {
        get { return nodeColor; }
        set { nodeObject.GetComponent<Renderer>().material.color = value; }
    }

    public bool IsNodeHaveObject
    {
        get { return isNodeHaveObject; }
        set { isNodeHaveObject = value; }
    }

    public Node(Vector3 positionWorld, int gridX, int gridZ, GameObject nodeObject, Color nodeColor)
    {
        this.positionWorld = positionWorld;
        this.gridX = gridX;
        this.gridZ = gridZ;
        this.nodeObject = nodeObject;
        this.nodeColor = nodeColor;
    }
}
