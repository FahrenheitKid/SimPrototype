using System.Collections;
using System.Collections.Generic;
using SimPrototype;
using UnityEngine;

[CreateAssetMenu(menuName = "Assets/Items/ItemData")]
public class ItemData : ScriptableObject
{
    public int ID; 
    public string Name;
    public Enums.ItemType ItemType;
    public Enums.ClothingType ClothingType;
    [TextArea]
    public string Description;
    public int Price;
    public int SellPriceModifier;
    public Sprite Icon;
    public string FirstFrameSpriteName;
}