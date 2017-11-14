using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// Class is is responsible for display item info under item object
/// </summary>
public class ItemInfo : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject itemInfoPanel;
    [SerializeField] private Text itemInfoText;
    [SerializeField] private ResourceObject item;

    private bool isActive = false;
    private bool canClick = false;

    void Start()
    {
        itemInfoPanel.SetActive(isActive);
        Setup();
    }

    /// <summary>
    /// Handle click on object
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        if (canClick)
        {
            isActive = !isActive;
            itemInfoPanel.SetActive(isActive);
        }
    }

    /// <summary>
    /// Initialization of item info
    /// </summary>
    void Setup()
    {
        itemInfoText.text = item.name;
        //canClick = item.IsBuilded;
    }

    /// <summary>
    /// Method activate/deactivate item info visualization
    /// </summary>
    /// <param name="isBuilded"></param>
    public void SetActive(bool isBuilded)
    {
        canClick = isBuilded;
    }
}
