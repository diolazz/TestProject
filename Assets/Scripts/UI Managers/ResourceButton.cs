using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class is responsible for manage item button in shop
/// </summary>
public class ResourceButton : MonoBehaviour {

    [SerializeField] private Button buttonComponent; //item button
    [SerializeField] private Text nameText; //item name text
    [SerializeField] private Image iconImage; //item icon

    private AudioSource source; //sound for click on button
    private ResourceObject item; //item object

    public delegate void OnButtonClick(ResourceObject itemToBuild); //event for handle click on button
    public event OnButtonClick onButtonClick;

    void Start()
    {
        source = GetComponent<AudioSource>();
        buttonComponent = GetComponent<Button>();
        buttonComponent.onClick.AddListener(OnButtonClicked);
    }

    /// <summary>
    /// Init button data
    /// </summary>
    /// <param name="currentItem"></param>
    public void Setup(ResourceObject currentItem)
    {
        item = currentItem;
        nameText.text = item.name;
        iconImage.sprite = item.icon;
    }

    /// <summary>
    /// Handle click on button
    /// </summary>
    public void OnButtonClicked()
    {
        if (onButtonClick != null)
        {
            onButtonClick(item);
            PlaySoundOnClick();
        }
    }

    public void PlaySoundOnClick()
    {
        if (source != null)
        {
            source.Play();
        }
    }
}
