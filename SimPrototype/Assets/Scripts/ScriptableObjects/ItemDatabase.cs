using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SimPrototype;
using UnityEngine;
using UnityEngine.U2D;

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
}