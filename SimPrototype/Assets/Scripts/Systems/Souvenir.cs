using System.Collections;
using System.Collections.Generic;
using SimPrototype;
using UnityEngine;

public class Souvenir : Item
{
    
    public Souvenir (int id, string name, Enums.ItemType itemType, string description, int price, float sellPriceModifier, Sprite icon, string firstFrameSpriteName)
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
    
    public Souvenir(Item souvenir)
    {
        ID = souvenir.ID;
        Name = souvenir.Name;
        ItemType = souvenir.ItemType;
        Description = souvenir.Description;
        Price = souvenir.Price;
        SellPriceModifier = souvenir.SellPriceModifier;
        Icon = souvenir.Icon;
        FirstFrameSpriteName = souvenir.FirstFrameSpriteName;
        ClothingType = souvenir.ClothingType;
    }

    public override void Use(Player player)
    {
        // souvenirs dont do any action
    }

}
