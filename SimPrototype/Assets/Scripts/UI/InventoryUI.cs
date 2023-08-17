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
    }

    public override void SelectItem(Item item)
    {
        base.SelectItem(item);
        if (item == null) return;
        //we update the buttons text and functions to reflect the new selected item
        _useButtonText.SetText(item.ItemType == Enums.ItemType.Clothing ? "Wear" : "Use");
        _useButton.onClick.RemoveAllListeners();
        _useButton.onClick.AddListener(delegate { item.Use(_playerRef); });
        _trashButton.onClick.RemoveAllListeners();
        _trashButton.onClick.AddListener(delegate { item.Trash(_playerRef); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
