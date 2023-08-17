using System;
using System.Collections;
using System.Collections.Generic;
using SimPrototype;
using UnityEngine;


[Serializable]
public abstract class Item
{

    [field: SerializeField] public int ID { get; protected set; } 
    [field: SerializeField] public string Name { get; protected set; }
    [field: SerializeField] public Enums.ItemType ItemType { get; protected set; }
    [field: SerializeField] public Enums.ClothingType ClothingType { get; protected set; }
    [field: SerializeField] public string Description { get; protected set; }
    [field: SerializeField] public int Price { get; protected set; }
    [field: SerializeField] public int SellPriceModifier { get; protected set; }
    [field: SerializeField] public Sprite Icon { get; protected set; }
    [field: SerializeField] public string FirstFrameSpriteName { get; protected set; }

    public Item(){}
    public Item(int id, string name, Enums.ItemType itemType, string description, int price, int sellPriceModifier, Sprite icon, string firstFrameSpriteName, Enums.ClothingType clothingType)
    {
        ID = id;
        Name = name;
        ItemType = itemType;
        Description = description;
        Price = price;
        SellPriceModifier = sellPriceModifier;
        Icon = icon;
        FirstFrameSpriteName = firstFrameSpriteName;
        ClothingType = clothingType;
    }

    public abstract void Buy(Player buyer, ShopUI seller);
    public abstract void Sell(Player seller, ShopUI buyer);
    public abstract void Use(Player player);

    public virtual void Trash(Player player)
    {
        player.RemoveItem(this);
    }

}

public interface IConsumable
{
    public void Consume(Player player);
}

public interface IWearable
{
    public void Wear(Player player);
}