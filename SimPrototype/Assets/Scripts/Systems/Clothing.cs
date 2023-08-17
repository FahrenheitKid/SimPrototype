using System;
using System.Collections;
using System.Collections.Generic;
using SimPrototype;
using UnityEngine;
using static UtilityTools.UtilityTools;

[Serializable]
public class Clothing : Item, IWearable
{
    // the spritesheets used are not organized one color after the other like
    // 1,1,1,1,1,1,2,2,2,2,2,2,3,3,3,3,3,3
    // 4,4,4,4,4,4,5,5,5,5,5,5,6,6,6,6,6,6
    // but as 1,1,1,1,1,1,1,2,2,2,2,2,2,2,3,3,3,3,3,3,3
    //        1,1,1,1,1,1,1,2,2,2,2,2,2,2,3,3,3,3,3,3,3
    // so after in our case, sprites of the same animation are "connected" for 8 frames then they "jump" 105 frames
    // we use these values to do some math workaround
    
    public const int AnimationFramesConnectedSize = 8;
    public const int AnimationSpriteSheetJump = 73;
    public const int AnimationSpriteSheetJumpHair = 105;
    public const int AnimationSpriteSheetTotalRowJumps = 3;

    public Clothing (int id, string name, Enums.ItemType itemType, string description, int price, int sellPriceModifier, Sprite icon, string firstFrameSpriteName, Enums.ClothingType clothingType)
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
        Wear(player);
    }

    public void Wear(Player player)
    {
        player.Wear(this);
    }

    public int GetStartingSpriteSheetIndex()
    {
        // string lastChar = FirstFrameSpriteName[FirstFrameSpriteName.Length - 1].ToString();
        //bool success = int.TryParse(lastChar, out int index); // check if last char is a number
        return GetLastNumberFromString(FirstFrameSpriteName);

    }

    public string GetFirstSpriteName(bool noNumber)
    {
        return noNumber ? GetStringWithoutLastNumber(FirstFrameSpriteName) : FirstFrameSpriteName;
    }
}
