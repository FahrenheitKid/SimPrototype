using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SimPrototype;
using UnityEngine;
using UnityEngine.U2D;
using Random = UnityEngine.Random;

[CreateAssetMenu(menuName = "Assets/Items/Item Database")]
public class ItemDatabase : ScriptableObject
{
    public List<ItemData> Items;
    public SpriteAtlas CharacterSpriteAtlas;
    public SpriteAtlas NonWearableSpriteAtlas;
    public SpriteAtlas UISpriteAtlas;

    public Clothing GetClothingByID(int id)
    {
        ItemData clothingData = Items.FirstOrDefault(x => x.ID == id);

        if (clothingData != null)
        {
            return ConvertItemDataToClothing(clothingData);
        }
        else
        {
            return null;
        }

    }

    public Clothing GetRandomClothing(Enums.ClothingType clothingType)
    {
        List<ItemData> filteredClothing = Items.FindAll(x => x.ClothingType == clothingType);
        ItemData clothingData = filteredClothing.ElementAtOrDefault(Random.Range(0, filteredClothing.Count));
        
        if (clothingData != null)
        {
            return ConvertItemDataToClothing(clothingData);
        }
        else
        {
            return null;
        }
    }

    public List<Item> GetAllItems()
    {
        List<Item> result = new List<Item>();

        foreach (ItemData data in Items)
        {
            result.Add(ConvertItemDataToItem(data));
        }
        
        return result;
    }

    public static Clothing ConvertItemDataToClothing(ItemData itemData)
    {
        if (itemData.ItemType != Enums.ItemType.Clothing || itemData == null) return null;
        
        return new Clothing(itemData.ID, itemData.Name,itemData.ItemType,itemData.Description,
            itemData.Price,itemData.SellPriceModifier,itemData.Icon,itemData.FirstFrameSpriteName, itemData.ClothingType);
    }
    public static Consumable ConvertItemDataToConsumable(ItemData itemData)
    {
        if (itemData.ItemType != Enums.ItemType.Consumable || itemData == null) return null;
        
        return new Consumable(itemData.ID, itemData.Name,itemData.ItemType,itemData.Description,
            itemData.Price,itemData.SellPriceModifier,itemData.Icon,itemData.FirstFrameSpriteName, 0);
    }
    public static Souvenir ConvertItemDataToSouvenir(ItemData itemData)
    {
        if (itemData.ItemType != Enums.ItemType.Souvenir || itemData == null) return null;
        
        return new Souvenir(itemData.ID, itemData.Name,itemData.ItemType,itemData.Description,
            itemData.Price,itemData.SellPriceModifier,itemData.Icon,itemData.FirstFrameSpriteName);
    }
    public static Item ConvertItemDataToItem(ItemData itemData)
    {
        if (itemData == null) return null;
        Item item;

        switch (itemData.ItemType)
        {
            case Enums.ItemType.Clothing:
                item = ConvertItemDataToClothing(itemData);
                break;
            case Enums.ItemType.Consumable:
                item = ConvertItemDataToConsumable(itemData);
                break;
            case Enums.ItemType.Souvenir:
                item = ConvertItemDataToSouvenir(itemData);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        return item;
    }
}