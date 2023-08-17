using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup parentObject;
    [SerializeField] private TextMeshProUGUI _selectedItemTitle;
    [SerializeField] private TextMeshProUGUI _selectedItemDescription;
    [SerializeField] private Image _selectedItemImage;

    public virtual void ShowMenu(bool on)
    {
        parentObject.DOFade(on? 1f : 0, 1f);
        parentObject.interactable = on;
        parentObject.blocksRaycasts = on;
        
    }

    public virtual void SelectItem(Item item)
    {
        if (item == null)
        {
            if(_selectedItemTitle)
                _selectedItemTitle.SetText("");
        
            if(_selectedItemDescription)
                _selectedItemDescription.SetText("");

            if (_selectedItemImage)
                _selectedItemImage.sprite = null;
            
            return;
        }
        
        if(_selectedItemTitle)
            _selectedItemTitle.SetText(item.Name);
        
        if(_selectedItemDescription)
            _selectedItemDescription.SetText(item.Description);

        if (_selectedItemImage)
            _selectedItemImage.sprite = item.Icon;
    }

    protected MenuItemButton CreateMenuItemButton(GameObject prefab, Item item, Transform contentParent, string gameObjectNameAddition)
    {
        MenuItemButton menuItem = Instantiate(prefab).GetComponent<MenuItemButton>();
        menuItem.transform.SetParent(contentParent.transform);
        menuItem.Setup(item);
        menuItem.gameObject.name = item.Name + gameObjectNameAddition;
        
        return menuItem;

    }
    
}
