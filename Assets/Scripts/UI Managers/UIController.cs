using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// UI manager
/// </summary>
public class UIController : Singleton<UIController>
{

    [SerializeField] private GameObject gameMenuPanel; //game panel object
    [SerializeField] private GameObject shopPanel;//shop panel object

    [SerializeField] private Button gridButton; // button for activate/deactivate grid
    [SerializeField] private Button shopButton;// shop button

    private GridController gridController;
    private ShopController shopController;

    private void Start()
    {
        gridController = GridController.Instance;
        shopController = ShopController.Instance;
 
        gridButton.onClick.AddListener(OnGridButtonClicked);
        shopButton.onClick.AddListener(OnShopButtonClicked);
    }

    public void ShowShopPanel()
    {
        shopPanel.SetActive(true);
    }

    public void HideShopPanel()
    {
        shopPanel.SetActive(false);
    }


    public void ShowGameMenuPanel()
    {
        gameMenuPanel.SetActive(true);
    }

    public void HideGameMenuPanel()
    {
        gameMenuPanel.SetActive(false);
    }


    public void OnGridButtonClicked()
    {
        gridController.GridDisplay();
    }

    public void OnShopButtonClicked()
    {
        shopController.ShopDisplay();
    }
}
