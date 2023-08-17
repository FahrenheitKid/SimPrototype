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
    private Inventory PlayerInventory => _playerRef != null ? _playerRef.PlayerInventory : null;
    private Inventory ShopInventory => _shopRef != null ? _shopRef.ShopInventory : null;
    [SerializeField] private GameObject _shopButtonPrefab;
    [SerializeField] private GameObject shopContentParent;
    [SerializeField] private GameObject playerContentParent;
    [SerializeField] private Button _buyButton;
    [SerializeField] private Button _sellButton;
    [SerializeField] private TextMeshProUGUI _buyPriceText;
    [SerializeField] private TextMeshProUGUI _sellPriceText;
    [SerializeField] private TextMeshProUGUI _moneyText;
    [SerializeField] private Player _playerRef;
    [SerializeField] private Shop _shopRef;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public override void ShowMenu(bool on)
    {
        base.ShowMenu(on);
        SelectItem(PlayerInventory.Items.FirstOrDefault(),true);
        UpdatePlayerMoneyText();
        
        //we can only update the player inventory list since shop inventory stays the same
        PopulateListFromInventory(true);
    }

    public void Setup(Shop shop, Player player)
    {
        _shopRef = shop;
        _playerRef = player;
        
        PopulateListsFromInventories();
    }

    void PopulateListsFromInventories()
    {
        ClearUILists();
        PopulateListFromInventory(true);
        PopulateListFromInventory(false);
    }

    void PopulateListFromInventory(bool playerList)
    {
        // if bool playerList not the player, it is the shops
        if (playerList)
        {
            foreach (Item item in PlayerInventory.Items)
            {
                MenuItemButton menuItem = CreateMenuItemButton(_shopButtonPrefab,item,playerContentParent.transform," | Player Inventory Menu Item Button");
                menuItem.AddListenerOnClick(delegate { SelectItem(menuItem.ButtonItem,false);});
            }
        }
        else
        {
            foreach (Item item in ShopInventory.Items)
            {
            
                MenuItemButton menuItem = CreateMenuItemButton(_shopButtonPrefab,item,shopContentParent.transform," | Shop Inventory Menu Item Button");
                menuItem.AddListenerOnClick(delegate { SelectItem(menuItem.ButtonItem,true);});
            
            }
        }
        
    }
    
    void ClearUILists()
    {
        ClearUIList(true);
        ClearUIList(false);
    }

    void ClearUIList(bool playerList)
    {
        //if playerList bool is not the player, it is the shops
        if (playerList)
        {
            foreach (Transform child in playerContentParent.transform)
            {
                Destroy(child.gameObject);
            }
        }
        else
        {
            foreach (Transform child in shopContentParent.transform)
            {
                Destroy(child.gameObject);
            }
        }
        
    }

    public void SelectItem(Item item, bool fromShop)
    {
        base.SelectItem(item);
        
        if(item== null) return;
        //we update the buttons text and functions to reflect the new selected item
        _buyPriceText.SetText((item.Price).ToString());
        _sellPriceText.SetText(((int)(item.SellPriceModifier * item.Price)).ToString());
        _buyButton.onClick.RemoveAllListeners();
        _sellButton.onClick.RemoveAllListeners();
        
        bool hasItem = PlayerInventory.HasItem(item) == false;
        
        var colors = _sellButton.colors;
        colors.normalColor = new Color(255f,255f,255f,hasItem ? 255f : 255f/5f);
        _sellButton.colors = colors;
        colors.normalColor = new Color(255f,255f,255f,fromShop ? 255f : 255f/5f);
        _buyButton.colors = colors;
            
        if(hasItem && !fromShop) // only sell if we have the item
            _sellButton.onClick.AddListener(delegate { TryTransaction(item,_sellButton,false); });
        
        if(fromShop)
            _buyButton.onClick.AddListener(delegate { TryTransaction(item, _buyButton,true); });
            
        
        
    }

    void TryTransaction(Item item, Button button, bool buy)
    {
        //buy == true is buy, the opposite is sell
        bool success = buy ? item.Buy(_playerRef, _shopRef) : item.Sell(_playerRef, _shopRef);

        if (success == false)
        {
            button.transform.DOShakeRotation(0.5f,90f,10,90f,true,ShakeRandomnessMode.Harmonic);
        }
        else
        {
            UpdatePlayerMoneyText();
        }
        
    }

    public void UpdatePlayerMoneyText()
    {
        if (_moneyText != null && _playerRef != null) 
            _moneyText.SetText("Money \n" + _playerRef.Money);
    }
}
