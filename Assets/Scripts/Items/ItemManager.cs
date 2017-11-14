using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class container-manager for all items objects
/// </summary>
public class ItemManager : Singleton<ItemManager>
{

    [SerializeField] private List<ResourceObject> resourceObjects;
    
    public List<ResourceObject> ItemObjects
    {
        get { return resourceObjects; }
    }

}
