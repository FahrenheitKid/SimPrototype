using System.Collections;
using System.Collections.Generic;
using SimPrototype;
using UnityEngine;

public class Clothing : Item, IWearable
{
    [SerializeField] private Enums.ClothingType _clothingType;
    [SerializeField] private string firstFrameSpriteName;
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

    public void Wear(Player player)
    {
        throw new System.NotImplementedException();
    }
}
