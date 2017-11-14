using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// SpawmManager class for randomly generate items on the field
/// </summary>
public class SpawnManager : Singleton<SpawnManager>
{
    [Tooltip("Number of lines")]
    public int count = 3; 
    [Tooltip("The higher the value the less the density")]
    public int density = 3;

    private Node[,] grid;
    private int gridX;
    private int gridZ;

    private BuildManager buildManager;
    private List<ResourceObject> itemsList; //list of all items

    void Start()
    {
        buildManager = BuildManager.Instance;
        itemsList = ItemManager.Instance.ItemObjects;

        grid = GridGenerator.Instance.Cells;
        gridX = grid.GetLength(0);
        gridZ = grid.GetLength(1);
        SpawnOnEdge();
    }

    
    /// <summary>
    /// Generate items on the field
    /// </summary>
    private void SpawnOnEdge()
    {
        int xMin = 0;
        int zMin = 0;
        int xMax = grid.GetLength(0);
        int zMax = grid.GetLength(1);
        
        for (int x = 0; x < xMax; x++)
        {
            for (int z = 0; z < zMax; z++)
            {
                if(((x > xMin + count - 1) && (x < xMax - count))&& ((z > zMin + count - 1)&&( z < zMax - count)))
                {
                   continue;
                }

                ResourceObject newObject = itemsList[Random.Range(0, itemsList.Count)];
                Vector3 position = grid[x, z].PositionWorld;

                if (Random.Range(0, density) == 1 && buildManager.IsLegalPositionToBuild(position, newObject.size ))
                {
                    GameObject newCell = Instantiate(newObject.prefab, position, grid[x,z].NodeObject.transform.rotation);

                    //grid[x, z].ObjectOnNode = newObject;
                    buildManager.SetItemToGrid(grid[x, z], newObject, newCell.transform);
                }
                

            }
        }
    }
 
}
