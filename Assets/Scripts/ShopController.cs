using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Shop manager
/// </summary>
public class ShopController : Singleton<ShopController>
{
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private GameObject buttonPrefab;

    private List<ResourceObject> itemsList; 
    private UIController uiController;
    private BuildManager buildManager;

    private void Start()
    {
        uiController = UIController.Instance;
        buildManager = BuildManager.Instance;
        itemsList = ItemManager.Instance.ItemObjects;

        InitButtons();
    }

    /// <summary>
    /// Create item button
    /// </summary>
    private void InitButtons()
    {
        foreach (var item in itemsList)
        {
            //ResourceObject item = itemsList[i];
            GameObject newButton = Instantiate(buttonPrefab);
            newButton.transform.SetParent(shopPanel.transform);

            ResourceButton resourceButton = newButton.GetComponent<ResourceButton>();
            resourceButton.Setup(item);
            resourceButton.onButtonClick += BuildButtonClicked;
        }
    }

    /// <summary>
    /// Open shop
    /// </summary>
    public void ShopDisplay()
    {
        uiController.HideGameMenuPanel();
        uiController.ShowShopPanel();

        //pause game
        Time.timeScale = 0;
    }

    /// <summary>
    /// Handle click on button
    /// </summary>
    /// <param name="item"></param>
    public void BuildButtonClicked(ResourceObject item)
    {
        uiController.HideShopPanel();
        uiController.ShowGameMenuPanel();
        buildManager.SetItem(item);

        Time.timeScale = 1;
    }
}
