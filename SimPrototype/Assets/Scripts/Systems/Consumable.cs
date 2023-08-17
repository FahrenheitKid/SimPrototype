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
    
    public Consumable(Item consumable)
    {
        ID = consumable.ID;
        Name = consumable.Name;
        ItemType = consumable.ItemType;
        Description = consumable.Description;
        Price = consumable.Price;
        SellPriceModifier = consumable.SellPriceModifier;
        Icon = consumable.Icon;
        FirstFrameSpriteName = consumable.FirstFrameSpriteName;
        ClothingType = consumable.ClothingType;
    }

    public override void Use(Player player)
    {
        //here in use maybe in the future can do more work like, can consume a number of times and do the internal maintenance of that
        Consume(player);
    }

    public void Consume(Player player)
    {
        //here usually we would do some health modification for player, add temporary buffs etc
        //then normally trash the item right after
        
        Trash(player);
    }
}
