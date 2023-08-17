using System.Collections;
using System.Collections.Generic;
using SimPrototype;
using UnityEngine;

public class Consumable : Item, IConsumable
{
    private int _healthModification;
    
    public Consumable (int id, string name, Enums.ItemType itemType, string description, int price, float sellPriceModifier, Sprite icon, string firstFrameSpriteName, int healthModification)
    {
        ID = id;
        Name = name;
        ItemType = itemType;
        Description = description;
        Price = price;
        SellPriceModifier = sellPriceModifier;
        Icon = icon;
        FirstFrameSpriteName = firstFrameSpriteName;
        _healthModification = healthModification;
    }
    public override void Buy(Player buyer, ShopUI seller)
    {
        throw new System.NotImplementedException();
    }

    public override void Sell(Player seller, ShopUI buyer)
    {
        throw new System.NotImplementedException();
    }

    public override void Use(Player player)
    {
        throw new System.NotImplementedException();
    }

    public override void Trash(Player player)
    {
        throw new System.NotImplementedException();
    }

    public void Consume(Player player)
    {
        throw new System.NotImplementedException();
    }
}
