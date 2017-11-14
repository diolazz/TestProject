using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Build manager
/// </summary>
public class BuildManager : Singleton<BuildManager>
{
    [SerializeField] private LayerMask layer; //layer mask for build
    [SerializeField] private GameObject buildEffectPrefab; // build effect prefab

    private Transform currentObjectToBuild;//object to build
    private ResourceObject currentItem;//item 
    private Vector3 currentPosition;
    private bool hasBuild;

    private Node[,] cells;//grid
    private int gridX;
    private int gridZ;

    private float distance = 100;

    private List<Node> previousNodes;
    private List<Node> nodes;
    private Color defaultColor;
    private Color validColor;
    private Color inValidColor;


    private GameObject holder;
    private string holderName = "Items";

    private GridGenerator grid;

    private void Start()
    {
        grid = GridGenerator.Instance;
        cells = grid.Cells;
        gridX = cells.GetLength(0);
        gridZ = cells.GetLength(1);
        previousNodes = new List<Node>();

        defaultColor = Color.gray;
        validColor = Color.green;
        inValidColor = Color.red;
    }

    void Update()
    {
        if (currentObjectToBuild != null && !hasBuild)
        {
            FollowMousePosition();

            if (Input.GetMouseButton(0))
            {
                Build();
            }
        }

    }
    /// <summary>
    /// Build object on the field
    /// </summary>
    private void Build()
    {
        if (IsLegalPositionToBuild(currentPosition, currentItem.size))
        {
            hasBuild = true;
            currentObjectToBuild.position = grid.NodeFromWorldPoint(currentPosition).PositionWorld;
            currentPosition = currentObjectToBuild.position;
            SetItemToGrid(grid.NodeFromWorldPoint(currentPosition), currentItem, currentObjectToBuild);
            GameObject effect = Instantiate(buildEffectPrefab, currentPosition, Quaternion.identity);
            Destroy(effect, 1f);
            ClearNodeColor(nodes, defaultColor);
        }
    }

    /// <summary>
    /// Link item to position on grid
    /// </summary>
    /// <param name="node"></param>
    /// <param name="item"></param>
    /// <param name="newObject"></param>
    public void SetItemToGrid(Node node, ResourceObject item, Transform newObject)
    {
        node.ObjectOnNode = item;
        if (holder == null)
        {
            holder = new GameObject();
            holder.name = holderName;
        }
        newObject.transform.SetParent(holder.transform);
        item.NodesOccupiedByObject = GridGenerator.Instance.GetNeighbours(node, item.size);
        newObject.GetComponent<ItemInfo>().SetActive(true);
        
    }

    /// <summary>
    /// Make object follow the mouse position
    /// </summary>
    private void FollowMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, distance, layer))
        {
            currentObjectToBuild.position = new Vector3(hitInfo.point.x, 0, hitInfo.point.z);
            currentPosition = currentObjectToBuild.position;

            ClearNodeColor(previousNodes, defaultColor);

            nodes = grid.GetNeighbours(grid.NodeFromWorldPoint(currentPosition), currentItem.size);
            if (grid.IsActive)
            {
                foreach (var node in nodes)
                {
                    if (node.IsNodeHaveObject)
                    {
                        node.NodeColor = inValidColor;
                        continue;
                    }

                    node.NodeColor = validColor;
                }
            }

            previousNodes = nodes;
        }
    }

    /// <summary>
    /// Set default color on node
    /// </summary>
    /// <param name="list"></param>
    /// <param name="newColor"></param>
    private void ClearNodeColor(List<Node> list, Color newColor )
    {
        if (grid.IsActive)
        {
            foreach (var node in list)
            {
                node.NodeColor = newColor;
            }
        }
    }

    /// <summary>
    /// Check is legal position to build object
    /// </summary>
    /// <param name="worldPosition"></param>
    /// <param name="itemSize"></param>
    /// <returns></returns>
    public bool IsLegalPositionToBuild(Vector3 worldPosition, int itemSize)
    {
        Node nodeOnWorldPosition = GridGenerator.Instance.NodeFromWorldPoint(worldPosition);
        List<Node> nodes = GridGenerator.Instance.GetNeighbours(nodeOnWorldPosition, itemSize);

        foreach (var node in nodes)
        {
            if (node.IsNodeHaveObject)
            {
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// Set item to build
    /// </summary>
    /// <param name="item"></param>
    public void SetItem(ResourceObject item)
    {
        hasBuild = false;
        currentPosition = GetCenterPosition();
        currentItem = item;
        currentObjectToBuild = Instantiate(item.prefab).transform;
    }

    /// <summary>
    /// Center position on grid map
    /// </summary>
    /// <returns></returns>
    private Vector3 GetCenterPosition()
    {
        return cells[gridX/2, gridZ/2].PositionWorld;
    }

}