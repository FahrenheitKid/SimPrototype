using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using DG.Tweening;
using SimPrototype;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MenuUI
{

    [SerializeField] private Inventory _shopInventory;

    public Inventory PlayerInventory => _playerRef != null ? _playerRef.PlayerInventory : null;
    [SerializeField] private GameObject _shopButtonPrefab;
    [SerializeField] private GameObject shopContentParent;
    [SerializeField] private GameObject playerContentParent;
    [SerializeField] private Button _buyButton;
    [SerializeField] private Button _sellButton;
    [SerializeField] private TextMeshProUGUI _buyPriceText;
    [SerializeField] private TextMeshProUGUI _sellPriceText;
    [SerializeField] private TextMeshProUGUI _moneyText;
    [SerializeField] private Player _playerRef;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public override void ShowMenu(bool on)
    {
        base.ShowMenu(on);
        SelectItem(PlayerInventory.Items.FirstOrDefault(),true);
        _moneyText.SetText("Money \n" + _playerRef.Money);
    }

    public void Setup(Inventory shopInventory, Player player)
    {
        _shopInventory = shopInventory;
        _playerRef = player;
        
        PopulateListsFromInventories();
    }

    void PopulateListsFromInventories()
    {
        ClearUILists();
        foreach (Item item in _shopInventory.Items)
        {
            
            MenuItemButton menuItem = CreateMenuItemButton(_shopButtonPrefab,item,shopContentParent.transform," | Shop Inventory Menu Item Button");
            menuItem.AddListenerOnClick(delegate { SelectItem(menuItem.ButtonItem,true);});
            
        }
        foreach (Item item in PlayerInventory.Items)
        {
            MenuItemButton menuItem = CreateMenuItemButton(_shopButtonPrefab,item,playerContentParent.transform," | Player Inventory Menu Item Button");
            menuItem.AddListenerOnClick(delegate { SelectItem(menuItem.ButtonItem,false);});
        }
    }
    
    void ClearUILists()
    {
        foreach (Transform child in shopContentParent.transform)
        {
            Destroy(child.gameObject);
        }
        
        foreach (Transform child in playerContentParent.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void SelectItem(Item item, bool fromShop)
    {
        base.SelectItem(item);
        
        if(item== null) return;
        //we update the buttons text and functions to reflect the new selected item
        _buyPriceText.SetText((item.Price).ToString());
        _sellPriceText.SetText((item.SellPriceModifier * item.Price).ToString());
        _buyButton.onClick.RemoveAllListeners();
        _sellButton.onClick.RemoveAllListeners();
        
        bool hasItem = PlayerInventory.HasItem(item) == false;
        
        var colors = _sellButton.colors;
        colors.normalColor = new Color(255f,255f,255f,hasItem ? 255f : 255f/5f);
        _sellButton.colors = colors;
        colors.normalColor = new Color(255f,255f,255f,fromShop ? 255f : 255f/5f);
        _buyButton.colors = colors;
            
        if(hasItem && !fromShop) // only sell if we have the item
            _sellButton.onClick.AddListener(delegate { item.Sell(_playerRef,this); });
        
        if(fromShop)
            _buyButton.onClick.AddListener(delegate { item.Buy(_playerRef,this); });
            
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
