using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// GridGenerator class is responsible for generate grid
/// </summary>
public class GridGenerator : Singleton<GridGenerator>
{
    [SerializeField] private GameObject planeTarget; //target object 
    [SerializeField] private GameObject cellPrefab; // grid's tile prefab
    [SerializeField] private int cellSize = 1; // size of tile
    [SerializeField] private float yOffset = 0.01f; 

    private Color defaultColor; // default color of tile

    private int gridWidth; //grid size
    private int gridHeight; //grid size

    private Vector3 startPosition; //start point for grid
    private Vector3 origin; // position of target object

    private Node[,] cells; //grid array

    private GameObject gridHolder; //object for holder grid in hierarchy
    private string gridHolderName = "Grid"; //name of holder object


    public Node[,] Cells
    {
        get { return cells; }
    }

    private int gridSizeX, gridSizeZ;

    private bool isGridActive = true;

    public bool IsActive
    {
        get { return isGridActive; }
    }

    void Start()
    {
        gridHeight = (int) planeTarget.transform.localScale.x*10;
        gridWidth = (int) planeTarget.transform.localScale.z*10;

        gridSizeX = Mathf.RoundToInt(gridHeight / cellSize);
        gridSizeZ = Mathf.RoundToInt(gridWidth / cellSize);


        origin = planeTarget.transform.position;
        startPosition = origin - Vector3.right*gridHeight/2 - Vector3.forward*gridWidth/2 + Vector3.one*cellSize/2;

        defaultColor = Color.grey;

        InitGrid();
      
    }

    /// <summary>
    /// Method is response for grid creation
    /// </summary>
    void InitGrid()
    {
        cells = new Node[gridSizeX, gridSizeZ];

        if (gridHolder == null)
        {
            gridHolder = new GameObject();
            gridHolder.name = gridHolderName;
        }

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int z = 0; z < gridSizeZ; z++)
            {
                GameObject newCell = Instantiate(cellPrefab, new Vector3(startPosition.x + x * (float)cellSize, yOffset, startPosition.z + z * (float)cellSize), Quaternion.identity);
                newCell.transform.localScale *= cellSize;
                newCell.GetComponent<Renderer>().material.color = defaultColor;
                newCell.transform.parent = gridHolder.transform;
                cells[x, z] = new Node(newCell.transform.position, x, z, newCell, defaultColor);
            }
        }

    }

    /// <summary>
    /// returns all adjacent node
    /// </summary>
    /// <param name="node"></param>
    /// <param name="itemSize"></param>
    /// <returns></returns>
    public List<Node> GetNeighbours(Node node, int itemSize)
    {
        List<Node> neighbours = new List<Node>();

        int size = itemSize;

        if (size == 1)
        {
            neighbours.Add(node);
            return neighbours;
        }

        for (int x = -1; x <= size - 1; x++)
        {
            for (int z = -1; z <= size - 1; z++)
            {
                //center
               // if (x == 0 && z == 0)
                 //   continue;

                int checkX = node.GridX + x;
                int checkZ = node.GridZ + z;

                if (checkX >= 0 && checkX < gridSizeX && checkZ >= 0 && checkZ < gridSizeZ)
                {
                    neighbours.Add(cells[checkX, checkZ]);
                }
            }
        }

        return neighbours;
    }

    /// <summary>
    /// Convert position in world to node position in grid
    /// </summary>
    /// <param name="worldPosition"></param>
    /// <returns></returns>
    public Node NodeFromWorldPoint(Vector3 worldPosition)
    {
        float percentX = (worldPosition.x + gridHeight/2)/gridHeight;
        float percentY = (worldPosition.z + gridWidth/2)/gridWidth;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridHeight/cellSize - 1)*percentX);
        int z = Mathf.RoundToInt((gridWidth/cellSize - 1)*percentY);
        return cells[x, z];
    }


    /// <summary>
    /// Activate / deactivate grid on field
    /// </summary>
    /// <param name="isActive"></param>
    public void SetGridActive(bool isActive)
    {
        isGridActive = isActive;

        for (int x = 0; x < gridHeight / cellSize; x++)
        {
            for (int z = 0; z < gridWidth/cellSize; z++)
            {
                //cells[x, z].NodeObject.GetComponent<MeshRenderer>().enabled = isActive;
                if (isActive)
                {
                    cells[x, z].NodeColor = defaultColor;
                }
                else
                {
                    cells[x, z].NodeColor = Color.clear;
                }
            }
        }
    }
}
