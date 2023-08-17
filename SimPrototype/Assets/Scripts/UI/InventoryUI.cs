using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SimPrototype;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MenuUI
{
    public Inventory PlayerInventory => _playerRef != null ? _playerRef.PlayerInventory : null;
    [SerializeField] private GameObject _itemButtonPrefab;
    [SerializeField] private GameObject playerContentParent;

    [SerializeField] private Button _useButton;
    [SerializeField] private TextMeshProUGUI _useButtonText;
    [SerializeField] private Button _trashButton;

    [SerializeField] private Player _playerRef;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    public void Setup(Player player)
    {
        _playerRef = player;
        PopulateUIListFromInventory();
    }

    void PopulateUIListFromInventory()
    {
        ClearUIList();
        foreach (Item item in PlayerInventory.Items)
        {
            MenuItemButton menuItem = CreateMenuItemButton(_itemButtonPrefab,item,playerContentParent.transform," | Player Inventory Menu Item Button");
            menuItem.AddListenerOnClick(delegate { SelectItem(menuItem.ButtonItem);});
        }
    }

    void ClearUIList()
    {
        foreach (Transform child in playerContentParent.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public override void ShowMenu(bool on)
    {
        base.ShowMenu(on);
        SelectItem(PlayerInventory.Items.FirstOrDefault());
        PopulateUIListFromInventory();
    }

    public override void SelectItem(Item item)
    {
        base.SelectItem(item);
        if (item == null)
        {
            _useButton.onClick.RemoveAllListeners();
            _trashButton.onClick.RemoveAllListeners();
            return;
        }
        //we update the buttons text and functions to reflect the new selected item
        _useButtonText.SetText(item.ItemType == Enums.ItemType.Clothing ? "Wear" : "Use");
        _useButton.onClick.RemoveAllListeners();
        _useButton.onClick.AddListener(delegate { UseItem(item); });
        _trashButton.onClick.RemoveAllListeners();
        _trashButton.onClick.AddListener(delegate { ThrashItem(item);});
    }

    void UseItem(Item item)
    {
        if (item == null) return;
        if (item.ItemType == Enums.ItemType.Consumable) // if consumable we need to update list
        {
            //if fully consumed the item will be trashed so we update the list
            SelectItem(null);
            RemoveItemFromUIList(item); 
        }
        
        item.Use(_playerRef);
    }
    
    void ThrashItem(Item item) // removes from the UI and then trash it
    {
        if (item == null) return;
        
        SelectItem(null);
        RemoveItemFromUIList(item); 
        item.Trash(_playerRef);
    }

    void RemoveItemFromUIList(Item item)
    {
        if (item == null) return;
        
        foreach (Transform child in playerContentParent.transform)
        {
            MenuItemButton itemButton = child.GetComponent<MenuItemButton>();
            if(itemButton.ButtonItem == null) continue;
            if (item.ID == itemButton.ButtonItem.ID)
            {
                // remove parent then destroy gameobject
                itemButton.transform.SetParent(null);
                Destroy(itemButton.gameObject);
                return;
            }
        }
    }
}
