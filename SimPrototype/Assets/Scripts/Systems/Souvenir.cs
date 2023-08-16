using System.Collections;
using System.Collections.Generic;
using SimPrototype;
using UnityEngine;

public class Souvenir : Item
{
    
    public Souvenir (int id, string name, Enums.ItemType itemType, string description, int price, int sellPriceModifier, Sprite icon, string firstFrameSpriteName)
    {
        ID = id;
        Name = name;
        ItemType = itemType;
        Description = description;
        Price = price;
        SellPriceModifier = sellPriceModifier;
        Icon = icon;
        FirstFrameSpriteName = firstFrameSpriteName;
    }
    public override void Buy(Player buyer, Shop seller)
    {
        throw new System.NotImplementedException();
    }

    public override void Sell(Shop seller, Player buyer)
    {
        throw new System.NotImplementedException();
    }

    public override void Use()
    {
        throw new System.NotImplementedException();
    }

    public override void Trash()
    {
        throw new System.NotImplementedException();
    }
}
