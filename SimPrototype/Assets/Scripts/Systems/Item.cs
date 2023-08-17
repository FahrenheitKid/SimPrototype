using System;
using System.Collections;
using System.Collections.Generic;
using SimPrototype;
using UnityEditor;
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
    [field: SerializeField] public float SellPriceModifier { get; protected set; }
    [field: SerializeField] public Sprite Icon { get; protected set; }
    [field: SerializeField] public string FirstFrameSpriteName { get; protected set; }

    protected Item(){}
    protected Item(int id, string name, Enums.ItemType itemType, string description, int price, float sellPriceModifier, Sprite icon, string firstFrameSpriteName, Enums.ClothingType clothingType)
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

    public virtual bool Buy(Player buyer, Shop seller)
    {
        if (buyer == null) return false;
        if (buyer.Money < Price) return false;

        buyer.UpdateMoney(-Price);
        buyer.AddItem(Clone());
        
        return true;
    }
    public virtual bool Sell(Player seller, Shop buyer)
    {
        //for now the buyer is always a shop, doesnt have money and has infinite items
        // in the future here we can adjust money and inventory of the buyer too
        
        if (seller == null) return false;
        //if player doesnt have item can't sell it
        if(!seller.PlayerInventory.HasItem(this)) return false;
        
        Trash(seller); // remove from player inventory
        seller.UpdateMoney((int)(Price * SellPriceModifier));
        return true;

    }
    public abstract void Use(Player player);

    public virtual void Trash(Player player)
    {
        player.RemoveItem(this);
    }

    public Item Clone() // returns a new instance clone
    {
        switch (ItemType)
        {
            case Enums.ItemType.Clothing:
                return new Clothing(this);
                break;
            case Enums.ItemType.Consumable:
                return new Consumable(this);
                break;
            case Enums.ItemType.Souvenir:
                return new Souvenir(this);
                break;
            default:
                return null;
        }
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