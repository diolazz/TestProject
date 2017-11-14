using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Main item class. Contains all data of item
/// </summary>
[System.Serializable]
public class ResourceObject : ScriptableObject
{
    public int id;
    public string name;
    public int size;
    public Sprite icon;
    public GameObject prefab;

    private List<Node> nodesOccupiedByObject; //list of nodes that current item is occupy
    private bool isBuilded = false;

    public List<Node> NodesOccupiedByObject
    {
        get { return nodesOccupiedByObject; }
        set
        {
            nodesOccupiedByObject = value;
            foreach (var node in nodesOccupiedByObject)
            {
                node.IsNodeHaveObject = true;
            }
        }
    }

    public bool IsBuilded
    {
        get { return isBuilded; }
        set { isBuilded = value; }
    }
}
